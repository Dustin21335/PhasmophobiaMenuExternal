using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelStats : MemoryObject
    {
        public LevelStats(IntPtr address) : base(address) { }

        public string BoneRoom => new mString(Program.SimpleMemoryReading.ReadPointer(Address + 0xB8)).Value;
    }
}
