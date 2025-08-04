using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class SpeedHack : ToggleCheat
    {
        public IntValueSetting Speed = new IntValueSetting("Speed", 2);

        public override void OnDisable()
        {
            if (GameObjectManager.LocalPlayer != null) GameObjectManager.LocalPlayer.FirstPlayerController.WalkSpeed = 1.6f;
        }

        public override void Update()
        {
            if (GameObjectManager.LocalPlayer != null && GameObjectManager.LocalPlayer.FirstPlayerController.WalkSpeed != Speed.Value) GameObjectManager.LocalPlayer.FirstPlayerController.WalkSpeed = Speed.Value;
        }
    }
}
