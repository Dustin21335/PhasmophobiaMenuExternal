using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.GameSDK;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class FOVHack : ToggleCheat
    {
        public FloatValueSetting FOV = new FloatValueSetting("FOV", 90f);
        private float OriginalFOV = 1f;

        public override void OnEnable()
        {
            Player localPlayer = GameObjectManager.LocalPlayer;
            OriginalFOV = localPlayer != null ? localPlayer.FieldOfView : 90f;
        }

        public override void OnDisable()
        {
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (localPlayer != null && localPlayer.FieldOfView != OriginalFOV) localPlayer.FieldOfView = OriginalFOV;
            OriginalFOV = -1f;
        }

        public override void Update()
        {
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (localPlayer != null && OriginalFOV != -1f && localPlayer.FieldOfView != FOV.Value) localPlayer.FieldOfView = FOV.Value;
        }
    }
}
