using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class DebugTab : Tab
    {
        public DebugTab() : base("DebugTab", false) { }

        public override void Render()
        {
            UIUtil.TabItem("Debug Tab", () =>
            {

            });
        }
    }
}
