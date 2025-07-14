namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr GhostControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05E64D48, 0xC18, 0xB8, 0x20, 0x10, 0x28, 0x0);

        public static GhostTraits GhostTraits => new GhostTraits(GhostControllerPointer + 0x28);

        public static LevelController LevelController => new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
