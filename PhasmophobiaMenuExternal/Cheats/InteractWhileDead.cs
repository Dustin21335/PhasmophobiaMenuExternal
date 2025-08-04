using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.GameSDK;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class InteractWhileDead : ToggleCheat
    {
        public override void OnEnable()
        {
            FridaNetManager.HookMethod("PCPropGrabPlayerDied", 0xA97D00);
            FridaNetManager.OnHookMessageTriggered += OnHookMessage;
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (localPlayer != null) Enable(localPlayer.Address);
        }

        public override void OnDisable()
        {
            FridaNetManager.UnHookMethod("PCPropGrabPlayerDied");
            FridaNetManager.OnHookMessageTriggered -= OnHookMessage;
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (localPlayer != null) Disable(localPlayer.Address);
        }

        public override void OnApplicationQuit()
        {
            FridaNetManager.UnHookMethod("PCPropGrabPlayerDied");
            FridaNetManager.OnHookMessageTriggered -= OnHookMessage;
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (localPlayer != null) Disable(localPlayer.Address);
        }

        private void OnHookMessage(object? sender, FridaNetManager.HookMessage e)
        {
            if (Enabled) Enable(new IntPtr(Convert.ToInt64(e.Address, 16)));
        }

        private void Enable(IntPtr address)
        {
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (address == localPlayer?.Address && localPlayer.IsDead) localPlayer?.PCPropGrab?.PlayerRevived();
        }

        private void Disable(IntPtr address)
        {
            Player localPlayer = GameObjectManager.LocalPlayer;
            if (address == localPlayer?.Address && localPlayer.IsDead) localPlayer?.PCPropGrab?.PlayerDied();
        }
    }
}
