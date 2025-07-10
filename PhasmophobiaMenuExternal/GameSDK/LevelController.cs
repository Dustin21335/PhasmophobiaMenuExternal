namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelController
    {
        public IntPtr LevelControllerPointer => Program.SimpleMemoryReading.ReadPointer(GhostController.GhostControllerPointer + 0x78);

        public LevelRoom FavoriteRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(LevelControllerPointer + 0x38));
    }
}
