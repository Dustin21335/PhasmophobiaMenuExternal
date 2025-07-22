namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelRoom
    {
        public LevelRoom(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public string Name => new mString(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x60)).Value;

        public int TimePlayerBeenInRoom => Program.SimpleMemoryReading.Read<int>(Pointer + 0x74);

        public bool IsBasementOrAttic => Program.SimpleMemoryReading.Read<bool>(Pointer + 0x78);
    }
}
