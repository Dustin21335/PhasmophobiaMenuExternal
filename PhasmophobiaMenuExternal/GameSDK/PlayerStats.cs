using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStats : MemoryObject
    {
        public PlayerStats(IntPtr address) : base(address) { }

        public LevelStats LevelStats => new LevelStats(Program.SimpleMemoryReading.ReadPointer(Address + 0x40));
    }
}
