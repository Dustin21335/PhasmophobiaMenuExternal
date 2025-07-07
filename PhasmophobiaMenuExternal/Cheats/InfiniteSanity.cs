using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class InfiniteSanity : TaskCheat
    {
        public override Task Update()
        {
            if (Program.LocalPlayer != null && Program.LocalPlayer.PlayerSanity.Sanity != 100) Program.LocalPlayer.PlayerSanity.Sanity = 100f;
            return Task.CompletedTask;
        }
    }
}
