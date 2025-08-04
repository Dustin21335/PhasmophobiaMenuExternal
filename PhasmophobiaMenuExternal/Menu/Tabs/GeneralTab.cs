using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class GeneralTab : Tab
    {
        public GeneralTab() : base("GeneralTab") { }

        public override void Render()
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
        }
    }
}
