namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStats
    {
        public PlayerStats(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public LevelStats LevelStats => new LevelStats(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x40));
    }
}
