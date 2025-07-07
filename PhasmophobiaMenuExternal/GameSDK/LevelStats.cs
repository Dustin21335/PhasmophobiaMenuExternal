namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelStats
    {
        public LevelStats(IntPtr pointer)
        {
            LevelStatsPointer = pointer;
        }

        public IntPtr LevelStatsPointer;

        public string BoneRoom => new mString(Program.SimpleMemoryReading.ReadPointer(LevelStatsPointer + 0xB8)).Value;
    }
}
