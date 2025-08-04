using FridaNet;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace PhasmophobiaMenuExternal
{
    public class FridaNetManager
    {
        public class HookMessage
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Address")]
            public string Address { get; set; }
        }

        public class InvokeMessage
        {
            [JsonProperty("Result")]
            public string Result { get; set; }
        }

        private static FridaNetDeviceManager deviceManager = new FridaNetDeviceManager();
        private static FridaNetDevice device;
        private static FridaNetSession session;

        public static Dictionary<string, FridaNetScript> AllHooks = new Dictionary<string, FridaNetScript>();
        public static event EventHandler<HookMessage> OnHookMessageTriggered;

        public static void Initialize()
        {
            Console.WriteLine("Initializing hooks");
            device = deviceManager.GetLocalDevice();
            session = device.Attach(Program.SimpleMemoryReading.Process);
            Console.WriteLine("Completed initializing hooks");
        }

        public static void HookMethod(string hookName, IntPtr rvaOffset)
        {
            if (AllHooks.ContainsKey(hookName)) return;
            Console.WriteLine($"Hooking {hookName}");
            string hookMethodJavascript = $@"
                Interceptor.attach(Process.getModuleByName('GameAssembly.dll').base.add(ptr('0x{rvaOffset.ToInt64():X}')), {{
                    onEnter: function(args) {{
                        send({{
                            Name: '{hookName}',
                            Address: args[0].toString()
                        }});
                    }}
                }});
            ";
            FridaNetScript fridaNetScript = session.CreateScript(hookMethodJavascript, $"{hookName}");
            fridaNetScript.Message += OnHookMessage;
            fridaNetScript.Load();
            AllHooks.Add(hookName, fridaNetScript);
            Console.WriteLine($"Hooked {hookName}");
        }

        public static void UnHookMethod(string hookName)
        {
            if (!AllHooks.TryGetValue(hookName, out FridaNetScript? fridaNetScript) || fridaNetScript == null) return;
            Console.WriteLine($"Unhooking {hookName}");
            fridaNetScript.Message -= OnHookMessage;
            fridaNetScript.Unload();
            fridaNetScript.Dispose();
            AllHooks.Remove(hookName);
            Console.WriteLine($"Unhooked {hookName}");
        }

        public static async Task<IntPtr> InvokeMethod(string methodName, IntPtr instanceAddress, IntPtr rvaOffset, string? type = null, params IntPtr[] parameters)
        {
            TaskCompletionSource<IntPtr>? taskCompletionSource = type != null ? new TaskCompletionSource<IntPtr>() : null;
            List<string> fridaNetParameters = new List<string> { "'pointer'" };
            List<string> javascriptParameters = new List<string> { $"ptr('0x{instanceAddress:X}')" };
            foreach (IntPtr param in parameters)
            {
                fridaNetParameters.Add("'pointer'");
                javascriptParameters.Add($"ptr('0x{param.ToInt64():X}')");
            }
            string invokeMethodjavascript = $@"
                const address = Process.getModuleByName('GameAssembly.dll').base.add(ptr('0x{rvaOffset:X}'));
                const method = new NativeFunction(address, '{(type ?? "void")}', [{string.Join(", ", fridaNetParameters)}]);
                {(type != null ? $@"
                const result = method({string.Join(", ", javascriptParameters)});
                send({{ Result: result.toString() }});"
                : $@"method({string.Join(", ", javascriptParameters)});")}
            ";
            FridaNetScript fridaNetScript = session.CreateScript(invokeMethodjavascript, methodName);
            void ResultMessage(object? sender, FridaNetScriptMessageEventArgs e)
            {
                if (taskCompletionSource != null && e.MessageType == FridaNetMessageType.Send && e.Payload.Contains("Result"))
                {
                    InvokeMessage invokeMessage = JsonConvert.DeserializeObject<InvokeMessage>(e.Payload);
                    if (invokeMessage != null) taskCompletionSource.TrySetResult(new IntPtr(Convert.ToInt64(invokeMessage.Result, 16)));
                }
            }
            if (taskCompletionSource != null) fridaNetScript.Message += ResultMessage;
            fridaNetScript.Load();
            if (taskCompletionSource != null)
            {
                Task completed = await Task.WhenAny(taskCompletionSource.Task, Task.Delay(500));
                fridaNetScript.Message -= ResultMessage;
                fridaNetScript.Unload();
                return completed == taskCompletionSource.Task ? taskCompletionSource.Task.Result : IntPtr.Zero;
            }
            else
            {
                await Task.Delay(100);
                fridaNetScript.Unload();
                return IntPtr.Zero;
            }
        }

        private static void OnHookMessage(object sender, FridaNetScriptMessageEventArgs e)
        {
            if (e.MessageType == FridaNetMessageType.Send && e.Payload.Contains("Name") && e.Payload.Contains("Address"))
            {
                HookMessage hookMessage = JsonConvert.DeserializeObject<HookMessage>(e.Payload);
                if (hookMessage != null) OnHookMessageTriggered?.Invoke(sender, hookMessage);
            }
            else Console.WriteLine(e.Message);
        }

        public static IntPtr AllocateMemory<T>(T value) where T : struct
        {
            IntPtr address = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
            Marshal.StructureToPtr(value, address, false);
            return address;
        }

        public static IntPtr AllocateString(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            IntPtr address = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, address, bytes.Length);
            return address;
        }
    }
}