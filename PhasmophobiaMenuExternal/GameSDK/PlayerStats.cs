namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStats
    {
        public PlayerStats(IntPtr pointer)
        {
            PlayerStatsPointer = pointer;
        }

        public IntPtr PlayerStatsPointer;

        public LevelStats LevelStats => new LevelStats(Program.SimpleMemoryReading.ReadPointer(PlayerStatsPointer + 0x40));
    }
}
