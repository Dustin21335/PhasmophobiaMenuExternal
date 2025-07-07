namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr GhostControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05A46358, 0x58, 0x58, 0x178, 0x48, 0xB8, 0x20, 0x0);

        public static GhostTraits GhostTraits => new GhostTraits(GhostControllerPointer + 0x28);

        public static LevelController LevelController = new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
