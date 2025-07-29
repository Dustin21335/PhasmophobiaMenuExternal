using FridaNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PhasmophobiaMenuExternal
{
    public class Hooks
    {
        public class HookMessage
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }
        }

        private static FridaNetDeviceManager deviceManager = new FridaNetDeviceManager();
        private static FridaNetDevice device;
        private static FridaNetSession session;

        public static Dictionary<string, FridaNetScript> AllHooks = new Dictionary<string, FridaNetScript>();
        public static event EventHandler<HookMessage> HookTriggered;

        public static void Initialize()
        {
            Console.WriteLine("Initializing hooks");
            device = deviceManager.GetLocalDevice();
            session = device.Attach(Program.SimpleMemoryReading.Process);

            HookMethod("GhostAIStart", 0x1CD31F0);
            HookMethod("CursedItemsControllerStart", 0x1F57750);
            HookMethod("MapControllerStart", 0x201B940);
            Console.WriteLine("Completed initializing hooks");
        }

        public static void HookMethod(string hookName, IntPtr rvaOffset)
        {
            Console.WriteLine($"Hooking {hookName}");
            string hookJavascript = $@"
                Interceptor.attach(Process.getModuleByName('GameAssembly.dll').base.add(ptr('0x{rvaOffset.ToInt64():X}')), {{
                    onEnter: function(args) {{
                        send({{
                            Name: '{hookName}',
                            Address: args[0].toString()
                        }});
                    }}
                }});
            ";
            FridaNetScript fridaNetScript = session.CreateScript(hookJavascript, $"{hookName}");
            fridaNetScript.Message += OnMessage;
            fridaNetScript.Load();
            AllHooks.Add(hookName, fridaNetScript);
            Console.WriteLine($"Hooked {hookName}");
        }

        public static void UnHookMethod(string hookName)
        {
            Console.WriteLine($"Unhooking {hookName}");
            if (!AllHooks.TryGetValue(hookName, out FridaNetScript? fridaNetScript) || fridaNetScript == null) return;
            fridaNetScript.Message -= OnMessage;
            fridaNetScript.Unload();
            fridaNetScript.Dispose();
            AllHooks.Remove(hookName);
            Console.WriteLine($"Unhooked {hookName}");
        }

        private static void OnMessage(object sender, FridaNetScriptMessageEventArgs e)
        {
            JObject json = JObject.Parse(e.Message);
            string type = json["type"]?.ToString();
            if (type == "send")
            {
                HookMessage hookMessage = json["payload"].ToObject<HookMessage>();
                if (hookMessage != null) HookTriggered?.Invoke(sender, hookMessage);
            }
            else Console.WriteLine($"{type}");
        }
    }
}