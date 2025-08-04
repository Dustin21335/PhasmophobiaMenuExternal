using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class CursedItem : MemoryObject
    {
        public CursedItem(string name, IntPtr address) : base(address) 
        {
            Name = name;
        }

        public string Name;

        public Equipment Equipment => new Equipment(Address);
    }
}
