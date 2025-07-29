namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostActivity
    {
        public IntPtr Pointer => Program.SimpleMemoryReading.ReadPointer(GhostAI.Pointer + 0x58);

        public LevelController LevelController => new LevelController();
    }
}
