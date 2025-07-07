using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class SpeedHack : TaskCheat
    {
        public SpeedHack()
        {
            Value = 2; 
        }

        public override void OnDisable()
        {
            if (Program.LocalPlayer != null && Program.LocalPlayer.FirstPlayerController != null) Program.LocalPlayer.FirstPlayerController.WalkSpeed = 1.6f;
        }

        public override Task Update()
        {
            if (Program.LocalPlayer != null && Program.LocalPlayer.FirstPlayerController != null && Program.LocalPlayer.FirstPlayerController.WalkSpeed != Value) Program.LocalPlayer.FirstPlayerController.WalkSpeed = Value;
            return Task.CompletedTask;
        }
    }
}
