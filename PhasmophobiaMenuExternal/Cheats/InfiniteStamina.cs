using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class InfiniteStamina : ToggleCheat
    {
        public override void Update()
        {
            if (GameObjectManager.LocalPlayer != null && GameObjectManager.LocalPlayer.PlayerStamina.Stamina != 100f) GameObjectManager.LocalPlayer.PlayerStamina.Stamina = 100f;
        }
    }
}
