using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class CursedItem : MemoryObject
    {
        public CursedItem(IntPtr address) : base(address) { }

        public string Name;

        public Equipment Equipment => new Equipment(Address);
    }
}
