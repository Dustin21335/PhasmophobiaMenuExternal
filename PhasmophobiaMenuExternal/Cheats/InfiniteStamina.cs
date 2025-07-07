using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class InfiniteStamina : TaskCheat
    {
        public override Task Update()
        {
            if (Program.LocalPlayer != null && Program.LocalPlayer.PlayerStamina != null && Program.LocalPlayer.PlayerStamina.Stamina != 100f) Program.LocalPlayer.PlayerStamina.Stamina = 100f;
            return Task.CompletedTask;
        }
    }
}
