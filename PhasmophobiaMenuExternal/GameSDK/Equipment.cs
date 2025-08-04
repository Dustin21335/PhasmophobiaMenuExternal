using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Equipment : MemoryObject
    {
        public Equipment(IntPtr address) : base(address) { }

        public Prop Prop => new Prop(Address);
    }
}
