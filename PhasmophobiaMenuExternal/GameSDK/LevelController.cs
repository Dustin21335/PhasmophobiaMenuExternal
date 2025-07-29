namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelController
    {
        public IntPtr Pointer => Program.SimpleMemoryReading.ReadPointer(GhostAI.GhostActivity.Pointer + 0x40);

        public LevelRoom GhostCurrentRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x38));
    }
}
