namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelController
    {
        public IntPtr Pointer => Program.SimpleMemoryReading.ReadPointer(GhostController.Pointer + 0x78);

        public LevelRoom FavoriteRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x38));
    }
}
