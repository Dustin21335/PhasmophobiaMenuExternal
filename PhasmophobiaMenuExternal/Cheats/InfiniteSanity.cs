using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class InfiniteSanity : ToggleCheat
    {
        public override void Update()
        {
            if (GameObjectManager.LocalPlayer != null && GameObjectManager.LocalPlayer.PlayerSanity.Sanity != 100) GameObjectManager.LocalPlayer.PlayerSanity.Sanity = 100f;
        }
    }
}
