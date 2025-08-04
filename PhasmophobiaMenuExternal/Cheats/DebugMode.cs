using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Menu.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class DebugMode : ToggleCheat
    {
        public override void OnEnable()
        {
            Tab debugTab = HackMenu.Tabs.FirstOrDefault(t => t.Name == "DebugTab");
            if (debugTab != null) debugTab.Enabled = true;
        }

        public override void OnDisable()
        {
            Tab debugTab = HackMenu.Tabs.FirstOrDefault(t => t.Name == "DebugTab");
            if (debugTab != null) debugTab.Enabled = false;
        }
    }
}
