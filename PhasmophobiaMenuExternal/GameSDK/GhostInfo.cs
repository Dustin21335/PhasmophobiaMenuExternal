namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostInfo
    {
        public IntPtr GhostInfoPointer => Program.SimpleMemoryReading.ReadPointer(GhostController.GhostAI.GhostAIPointer + 0x38);

        public GhostTraits GhostTraits => new GhostTraits(GhostInfoPointer + 0x28);

        public LevelRoom FavoriteRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(GhostInfoPointer + 0x70));
    }
}
