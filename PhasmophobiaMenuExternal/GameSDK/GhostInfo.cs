namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostInfo
    {
        public IntPtr Pointer => Program.SimpleMemoryReading.ReadPointer(GhostAI.Pointer + 0x38);

        public GhostTraits GhostTraits => new GhostTraits();

        public LevelRoom FavoriteRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x70));
    }
}
