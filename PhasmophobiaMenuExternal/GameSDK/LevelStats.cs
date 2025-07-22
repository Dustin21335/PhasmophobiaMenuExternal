namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelStats
    {
        public LevelStats(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public string BoneRoom => new mString(Program.SimpleMemoryReading.ReadPointer(Pointer + 0xB8)).Value;
    }
}
