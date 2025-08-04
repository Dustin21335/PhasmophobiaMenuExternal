using ClickableTransparentOverlay;
using ImGuiNET;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Language;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;
using SimpleMemoryReading64and32;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PhasmophobiaMenuExternal
{
    public class Program : Overlay
    {
        public Program() : base("Phasmophobia Menu External", true, (int)ScreenSize.X, (int)ScreenSize.Y) { }

        private static Lazy<Program> instance = new Lazy<Program>(() => new Program());
        public static Program Instance => instance.Value;

        private static SimpleMemoryReading? simpleMemoryReading;

        public static SimpleMemoryReading SimpleMemoryReading
        {
            get
            {
                if (simpleMemoryReading == null)
                {
                    Process process = Process.GetProcessesByName("Phasmophobia").FirstOrDefault();
                    if (process != null) simpleMemoryReading = new SimpleMemoryReading(process);
                }
                return simpleMemoryReading;
            }
            set => simpleMemoryReading = value;
        }

        private static IntPtr gameAssembly;
        public static IntPtr GameAssembly
        {
            get
            {
                if (SimpleMemoryReading != null && gameAssembly == IntPtr.Zero) gameAssembly = SimpleMemoryReading.GetModuleBase("GameAssembly.dll");
                return gameAssembly;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        private static Thread renderThread = new Thread(() => Instance.Start().Wait());
        public static Vector2 ScreenSize;

        public static List<Cheat> allCheats = new List<Cheat>();
        public static List<ToggleCheat> toggleCheats = new List<ToggleCheat>();

        public static async Task Main()
        {
            Console.WriteLine("Waiting for Phasmophobia to load");
            while (SimpleMemoryReading == null || GameAssembly == IntPtr.Zero) await Task.Delay(1000);
            Console.WriteLine("Phasmophobia loaded");
            SimpleMemoryReading.Process.EnableRaisingEvents = true;
            SimpleMemoryReading.Process.Exited += (sender, e) => Environment.Exit(0);
            if (GetWindowRect(SimpleMemoryReading.Process.MainWindowHandle, out RECT rect)) ScreenSize = new Vector2(rect.Right - rect.Left, rect.Bottom - rect.Top);
            Localization.Initialize();
            FridaNetManager.Initialize();
            GameObjectManager.Initialize();
            HackMenu.Initialize();
            Assembly.GetExecutingAssembly()?.GetTypes()?.Where(t => !string.IsNullOrEmpty(t.Namespace) && !t.Namespace.Contains("Core") && t.IsSubclassOf(typeof(Cheat))).ToList().ForEach(t =>
            {
                Cheat cheat = (Cheat)Activator.CreateInstance(t);
                allCheats.Add(cheat);
                if (cheat is ToggleCheat toggleCheat)
                {
                    toggleCheats.Add(toggleCheat);
                    Console.WriteLine($"Loaded Toggle Cheat {t.Name}");
                }
                else Console.WriteLine($"Loaded Executable Cheat {t.Name}");
            });
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => toggleCheats.ToList().ForEach(c => c.OnApplicationQuit());
            Settings.Config.SaveDefaultConfig();
            Settings.Config.LoadConfig();
            Settings.Changelog.ReadChanges();
            Thread updateThread = new Thread(() =>
            {
                while (true)
                {
                    Update();
                    Thread.Sleep(32);
                }
            });
            updateThread.IsBackground = true;
            updateThread.Start();
            renderThread.Start();
            await Task.Delay(-1);
        }

        protected override void Render()
        {
            VisualUtil.DrawList = ImGui.GetBackgroundDrawList();
            UIUtil.Area($"Phasmophobia Menu External {Settings.Version}", () => UIUtil.TabBar("Phasmophobia Menu External", () => HackMenu.Tabs.Where(r => r.Enabled).ToList().ForEach(t => t.Render())), new Vector2(520, 320));
            toggleCheats.Where(c => c.Enabled).ToList().ForEach(c => c.OnGUI());
        }

        private static void Update()
        {
            toggleCheats.Where(c => c.Enabled).ToList().ForEach(c => c.Update());
        }
    }
}