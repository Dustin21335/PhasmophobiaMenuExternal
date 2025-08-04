using PhasmophobiaMenuExternal.Menu.Tabs;

namespace PhasmophobiaMenuExternal.Menu.Core
{
    public class HackMenu
    {
        public static List<Tab> Tabs = new List<Tab>();

        public static void Initialize()
        {
            Tabs.Add(new GeneralTab());
            Tabs.Add(new SelfTab());
            Tabs.Add(new MiscTab());
            Tabs.Add(new GhostTab());
            Tabs.Add(new PlayerTab());
            Tabs.Add(new SettingsTab());
            Tabs.Add(new DebugTab());
        }
    }
}
