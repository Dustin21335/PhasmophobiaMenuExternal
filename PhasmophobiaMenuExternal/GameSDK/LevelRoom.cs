using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelRoom : MemoryObject
    {
        public LevelRoom(IntPtr address) : base(address) { }

        public string Name => new mString(Program.SimpleMemoryReading.ReadPointer(Address + 0x60)).Value;

        public int TimePlayerBeenInRoom => Program.SimpleMemoryReading.Read<int>(Address + 0x74);

        public bool IsBasementOrAttic => Program.SimpleMemoryReading.Read<bool>(Address + 0x78);
    }
}
