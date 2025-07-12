using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.GameSDK;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class FOVHack : TaskCheat
    {
        public FOVHack() 
        {
            Value = 90f;
        }

        private float OriginalFOV = 1f;

        public override void OnEnable()
        {
            Player localPlayer = Program.LocalPlayer;
            OriginalFOV = localPlayer != null ? localPlayer.Camera.FieldOfView : 90f;
        }

        public override void OnDisable()
        {
            Player localPlayer = Program.LocalPlayer;
            if (localPlayer != null && localPlayer.Camera.FieldOfView != OriginalFOV) localPlayer.Camera.FieldOfView = OriginalFOV;
            OriginalFOV = -1f;
        }

        public override Task Update()
        {
            Player localPlayer = Program.LocalPlayer;
            if (localPlayer != null && OriginalFOV != -1f && localPlayer.Camera.FieldOfView != Value) localPlayer.Camera.FieldOfView = Value;
            return Task.CompletedTask;
        }
    }
}
