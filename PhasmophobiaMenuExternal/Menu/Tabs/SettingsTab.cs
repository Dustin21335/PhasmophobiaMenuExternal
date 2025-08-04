using PhasmophobiaMenuExternal.Cheats;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Language;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    internal class SettingsTab : Tab
    {
        public SettingsTab() : base("SettingsTab") { }

        public static string Language = "English";

        public override void Render()
        {
            UIUtil.TabItem("SettingsTab.Title", () =>
            {
                UIUtil.Button("SettingsTab.ResetSettings", Settings.Config.RegenerateConfig);
                UIUtil.SameLine();
                UIUtil.Button("SettingsTab.SaveSettings", Settings.Config.SaveConfig);
                UIUtil.Button("SettingsTab.ReloadSettings", Settings.Config.LoadConfig);
                UIUtil.SameLine();
                UIUtil.Button("SettingsTab.OpenSettings", Settings.Config.OpenConfig);
                UIUtil.DropDown("SettingsTab.Languages", ref Language, Localization.GetLanguages());
                UIUtil.Checkbox("SettingsTab.DebugMode", Cheat.Instance<DebugMode>());
                UIUtil.Text("SettingsTab.Colors");
                UIUtil.ColorPicker("SettingsTab.CrosshairColor", ref Cheat.Instance<Crosshair>().CrosshairColor.Value);
                UIUtil.Text("SettingsTab.ProjectInfo");
                UIUtil.Text("SettingsTab.CreatedBy", ": Dustin");
                UIUtil.Button("SettingsTab.Github", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/Dustin21335/PhasmophobiaMenuExternal", UseShellExecute = true }));
                UIUtil.SameLine();
                UIUtil.Button("SettingsTab.BuyMeACoffee", () => Process.Start(new ProcessStartInfo { FileName = "https://buymeacoffee.com/_dustin_", UseShellExecute = true }));
                UIUtil.Text("SettingsTab.Credits");
                UIUtil.Button("Clickable Transparent Overlay", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/zaafar/ClickableTransparentOverlay", UseShellExecute = true }));
                UIUtil.Button("ImGui.NET", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/ImGuiNET/ImGui.NET", UseShellExecute = true }));
                UIUtil.Button("Frida CLR", () => Process.Start(new ProcessStartInfo { FileName = "https://github.com/frida/frida-clr", UseShellExecute = true }));
            });
        }
    }
}
