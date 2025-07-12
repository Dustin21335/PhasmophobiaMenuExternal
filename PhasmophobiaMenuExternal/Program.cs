using ClickableTransparentOverlay;
using ImGuiNET;
using PhasmophobiaMenuExternal.Cheats;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.GameSDK;
using PhasmophobiaMenuExternal.Language;
using PhasmophobiaMenuExternal.Utils;
using SimpleMemoryReading64and32;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PhasmophobiaMenuExternal
{
    public class Program : Overlay
    {
        public Program() : base("Phasmophobia Menu", true, (int)ScreenSize.X, (int)ScreenSize.Y) { }

        private static Lazy<Program> instance = new Lazy<Program>(() => new Program());
        public static Program Instance => instance.Value;

        private static SimpleMemoryReading? simpleMemoryReading;

        public static SimpleMemoryReading SimpleMemoryReading
        {
            get
            {
                if (simpleMemoryReading == null)
                {
                    try
                    {
                        simpleMemoryReading = new SimpleMemoryReading("Phasmophobia");
                    }
                    catch { return null; }
                }
                return simpleMemoryReading;
            }
            set
            {
                simpleMemoryReading = value;
            }
        }

        private static IntPtr gameAssembly;
        public static IntPtr GameAssembly
        {
            get
            {
                if (gameAssembly == IntPtr.Zero)
                {
                    try
                    {
                        gameAssembly = SimpleMemoryReading.GetModuleBase("GameAssembly.dll");
                    }
                    catch { return IntPtr.Zero; }
                }
                return gameAssembly;
            }
            set
            {
                gameAssembly = value;
            }
        }

        private static IntPtr unityPlayer;
        public static IntPtr UnityPlayer
        {
            get
            {
                if (unityPlayer == IntPtr.Zero)
                {
                    try
                    {
                        unityPlayer = SimpleMemoryReading.GetModuleBase("UnityPlayer.dll");
                    }
                    catch { return IntPtr.Zero; }
                }
                return unityPlayer;
            }
            set
            {
                unityPlayer = value;
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

        public static Player? LocalPlayer => MapController.Players.FirstOrDefault(p => p.PhotonView.Owner.IsLocal);
        private static bool Loaded;
        private static Thread renderThread = new Thread(() => Instance.Start().Wait());
        public static Vector2 ScreenSize;
        public static string Language = "English";
        public static ImDrawListPtr DrawList;
        private static string posX = "0";
        private static string posY = "0";
        private static string posZ = "0";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Waiting for Phasmophobia to load");
            while (!Loaded)
            {
                Loaded = SimpleMemoryReading != null && GameAssembly != IntPtr.Zero && UnityPlayer != IntPtr.Zero;
                await Task.Delay(1000);
            }
            Console.WriteLine("Phasmophobia loaded");
            SimpleMemoryReading.process.EnableRaisingEvents = true;
            SimpleMemoryReading.process.Exited += (sender, e) => Environment.Exit(0);
            AppDomain.CurrentDomain.ProcessExit += (sender, e) => Cheat.instances.ToList().ForEach(c => c.OnApplicationQuit());
            if (GetWindowRect(SimpleMemoryReading.process.MainWindowHandle, out RECT rect)) ScreenSize = new Vector2(rect.Right - rect.Left, rect.Bottom - rect.Top);
            Localization.Initialize();
            Assembly.GetExecutingAssembly()?.GetTypes()?.Where(t => !string.IsNullOrEmpty(t.Namespace) && !t.Namespace.Contains("Core") && t.IsSubclassOf(typeof(Cheat))).ToList().ForEach(t => Activator.CreateInstance(t));
            Settings.Config.SaveDefaultConfig();
            Settings.Config.LoadConfig();
            Settings.Changelog.ReadChanges();
            renderThread.Start();
            await Task.Delay(-1);
        }


        protected override void Render()
        {
            DrawList = ImGui.GetBackgroundDrawList();
            GUI();
            Crosshair();
            GhostInfo();
            LevelInfo();
            PlayerInfo();
        }

        private void GUI()
        {
            UIUtil.Area($"Phasmophobia Menu {Settings.Version}", () =>
            {
                UIUtil.TabBar("Phasmophobia Menu", () =>
                {
                    UIUtil.TabItem("GeneralTab.Title", () =>
                    {
                        UIUtil.Text("Welcome to Phasmophobia Menu! Enjoy it and stay safe.");
                        UIUtil.Spacing(1);
                        List<IGrouping<string, Settings.Changelog.Entry>> versions = Settings.Changelog.entries.GroupBy(e => e.Version).ToList();
                        for (int i = 0; i < versions.Count; i++)
                        {
                            UIUtil.Text($"v{versions[i].Key}");
                            versions[i].ToList().ForEach(e => UIUtil.Text($"> {e.Type} {e.Name} - {e.Description}"));
                            UIUtil.Spacing(1);
                        }
                    });
                    UIUtil.TabItem("SelfTab.Title", () =>
                    {
                        UIUtil.SliderCheckbox("SelfTab.InfiniteSanity", Cheat.Instance<InfiniteSanity>(), 1, 5000);
                        UIUtil.SliderCheckbox("SelfTab.InfiniteStamina", Cheat.Instance<InfiniteStamina>(), 1, 5000);
                        UIUtil.SliderCheckbox("SelfTab.SpeedHack", Cheat.Instance<SpeedHack>(), 1, 5000);
                        UIUtil.Slider("SelfTab.Speed", Cheat.Instance<SpeedHack>(), 1, 25);
                        UIUtil.SliderCheckbox("SelfTab.GammaHack", Cheat.Instance<GammaHack>(), 500, 5000);
                        UIUtil.Slider("SelfTab.Gamma", Cheat.Instance<GammaHack>(), 1f, 4f);
                        UIUtil.SliderCheckbox("SelfTab.FOVHack", Cheat.Instance<FOVHack>(), 1, 5000);
                        UIUtil.Slider("SelfTab.FOV", Cheat.Instance<FOVHack>(), 10, 150);
                    });
                    UIUtil.TabItem("MiscTab.Title", () =>
                    {
                        UIUtil.Checkbox("MiscTab.GhostInfo", ref Settings.GhostInfo);
                        UIUtil.Checkbox("MiscTab.LevelInfo", ref Settings.LevelInfo);
                        UIUtil.Checkbox("MiscTab.PlayerInfo", ref Settings.PlayerInfo);
                        UIUtil.Checkbox("MiscTab.PlayerInfoSeperateWindows", ref Settings.PlayerInfoSeperateWindows);
                        UIUtil.Checkbox("MiscTab.Crosshair", ref Settings.Crosshair);
                        UIUtil.Slider("MiscTab.CrosshairSize", ref Settings.CrosshairSize, 1, 50);
                        UIUtil.Slider("MiscTab.CrosshairThickness", ref Settings.CrosshairThickness, 1, 25);
                        UIUtil.Text("MiscTab.Teleportion");
                        UIUtil.InputText("MiscTab.X", ref posX, 3);
                        UIUtil.InputText("MiscTab.Y", ref posY, 3);
                        UIUtil.InputText("MiscTab.Z", ref posZ, 3);
                        UIUtil.Button("MiscTab.SaveCurrentPosition", () =>
                        {
                            if (LocalPlayer == null) return;
                            posX = LocalPlayer.PlayerX.ToString();
                            posY = LocalPlayer.PlayerY.ToString();
                            posZ = LocalPlayer.PlayerZ.ToString();
                        });
                        UIUtil.SameLine();
                        UIUtil.Button("MiscTab.Teleport", () =>
                        {
                            if (float.TryParse(posX, out float x) && float.TryParse(posY, out float y) && float.TryParse(posZ, out float z) && LocalPlayer != null) LocalPlayer.PlayerPosition = new Vector3(x, y, z);
                        });
                    });
                    UIUtil.TabItem("SettingsTab.Title", () =>
                    {
                        UIUtil.Button("SettingsTab.ResetSettings", Settings.Config.RegenerateConfig);
                        UIUtil.SameLine();
                        UIUtil.Button("SettingsTab.SaveSettings", Settings.Config.SaveConfig);
                        UIUtil.Button("SettingsTab.ReloadSettings", Settings.Config.LoadConfig);
                        UIUtil.SameLine();
                        UIUtil.Button("SettingsTab.OpenSettings", Settings.Config.OpenConfig);
                        UIUtil.DropDown("SettingsTab.Languages", ref Language, Localization.GetLanguages());
                        UIUtil.Text("SettingsTab.Colors");
                        UIUtil.ColorPicker("SettingsTab.CrosshairColor", ref Settings.CrosshairColor);
                        UIUtil.Text("SettingsTab.ProjectInfo");
                        UIUtil.Text("SettingsTab.CreatedBy", ": Dustin");
                        UIUtil.Button("SettingsTab.Github", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/Dustin21335/PhasmophobiaMenuExternal", UseShellExecute = true }));
                        UIUtil.SameLine();
                        UIUtil.Button("SettingsTab.BuyMeACoffee", () => Process.Start(new ProcessStartInfo { FileName = "https://buymeacoffee.com/_dustin_", UseShellExecute = true }));           
                        UIUtil.Text("SettingsTab.Credits");
                        UIUtil.Button("SettingsTab.ClickableTransparentOverlay", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/zaafar/ClickableTransparentOverlay", UseShellExecute = true }));
                        UIUtil.Button("SettingsTab.ImGui.NET", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/ImGuiNET/ImGui.NET", UseShellExecute = true }));
                    });
                });
            });
        }

        private void Crosshair()
        {
            if (!Settings.Crosshair) return;
            float X = ScreenSize.X * 0.5f;
            float Y = ScreenSize.Y * 0.5f;
            float size = Settings.CrosshairSize;
            float thickness = Settings.CrosshairThickness;
            VisualUtil.DrawLine(new Vector2(X - size, Y), new Vector2(X + size, Y), Settings.CrosshairColor, thickness);
            VisualUtil.DrawLine(new Vector2(X, Y - size), new Vector2(X, Y + size), Settings.CrosshairColor, thickness);
        }

        public void GhostInfo()
        {
            if (!Settings.GhostInfo) return;
            UIUtil.Area("GhostInfo.Title", () =>
            {
                UIUtil.Text("GhostInfo.GhostName", $": {JournalController.GhostName.Text}");
                UIUtil.Text("GhostInfo.GhostType", $": {GhostController.GhostTraits.GhostType}");
                UIUtil.Text("GhostInfo.Evidence", $": {string.Join(", ", GhostController.GhostTraits.Evidences)}");
                UIUtil.Text("GhostInfo.FavoriteRoom", $": {GhostController.LevelController.FavoriteRoom.RoomName}");
                UIUtil.Text("GhostInfo.GhostState", $": {GhostController.GhostAI.GhostState}");
                UIUtil.Text("GhostInfo.IsHunting", $": {GhostController.GhostAI.IsHunting}");
            });
        }

        public void LevelInfo()
        {
            if (!Settings.LevelInfo) return;
            UIUtil.Area("LevelInfo.Title", () =>
            {
                UIUtil.Text("LevelInfo.BoneLocation", $": {(LocalPlayer != null ? LocalPlayer.PlayerStats.LevelStats.BoneRoom : "Unknown")}");
                UIUtil.Text("LevelInfo.CursedItem", $": {(CursedItemsController.CurrentCursedItem != null ? CursedItemsController.CurrentCursedItem.CursedItemName : "Unknown")}");
            });
        }

        public void PlayerInfo()
        {
            if (!Settings.PlayerInfo) return;
            Action<Player> ShowPlayerInfo = p =>
            {
                PhotonPlayer photonPlayer = p.PhotonView.Owner;
                if (!photonPlayer.IsMasterClient) UIUtil.SameLine();
                UIUtil.Group($"{photonPlayer.NickName} {photonPlayer.ActorNumber}", () =>
                {
                    UIUtil.Text("PlayerInfo.Name", $": {photonPlayer.NickName}");
                    UIUtil.Text("PlayerInfo.ActorNumber", $": {photonPlayer.ActorNumber}");
                    UIUtil.Text("PlayerInfo.CurrentRoom", $": {p.CurrentRoom.RoomName}");
                    UIUtil.Text("PlayerInfo.Stamina", $": {p.PlayerStamina.Stamina}");
                    UIUtil.Text("PlayerInfo.Sanity", $": {p.PlayerSanity.Sanity}");
                });
            };
            if (Settings.PlayerInfoSeperateWindows) MapController.Players.ForEach(p => UIUtil.Area("PlayerInfo.Title", () => ShowPlayerInfo(p)));
            else UIUtil.Area("PlayerInfo.Title", () => UIUtil.Group("PlayerInfoGroup", () => MapController.Players.ForEach(ShowPlayerInfo)));
        }
    }
}