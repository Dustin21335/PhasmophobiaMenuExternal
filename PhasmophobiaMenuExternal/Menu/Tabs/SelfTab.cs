using PhasmophobiaMenuExternal.Cheats;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class SelfTab : Tab
    {
        public SelfTab() : base("SelfTab") { }

        public override void Render()
        {
            UIUtil.TabItem("SelfTab.Title", () =>
            {
                UIUtil.Checkbox("SelfTab.InfiniteSanity", Cheat.Instance<InfiniteSanity>());
                UIUtil.Checkbox("SelfTab.InfiniteStamina", Cheat.Instance<InfiniteStamina>());
                UIUtil.SliderCheckbox("SelfTab.SpeedHack", Cheat.Instance<SpeedHack>(), ref Cheat.Instance<SpeedHack>().Speed.Value, 1, 25);
                UIUtil.SliderCheckbox("SelfTab.FOVHack", Cheat.Instance<FOVHack>(), ref Cheat.Instance<FOVHack>().FOV.Value, 10, 180);
                UIUtil.SliderCheckbox("SelfTab.GammaHack", Cheat.Instance<GammaHack>(), ref Cheat.Instance<GammaHack>().Gamma.Value, 1f, 4f);
                UIUtil.Checkbox("SelfTab.InteractableWhileDead", Cheat.Instance<InteractWhileDead>());
            });
        }
    }
}
