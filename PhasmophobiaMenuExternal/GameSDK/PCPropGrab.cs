using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PCPropGrab : MemoryObject
    {
        public PCPropGrab(IntPtr address) : base(address) { }

        public async void PlayerRevived()
        {
            await FridaNetManager.InvokeMethod("PCPropGrabPlayerRevived", Address, 0xA97DF0);
        }

        public async void PlayerDied()
        {
            await FridaNetManager.InvokeMethod("PCPropGrabPlayerDied", Address, 0xA97D00);
        }
    }
}
