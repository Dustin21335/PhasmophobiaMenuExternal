namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr GhostControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05BDDCF0, 0xA8, 0x20, 0x1B0, 0x40, 0xB8, 0x20, 0x0);

        public static GhostTraits GhostTraits => new GhostTraits(GhostControllerPointer + 0x28);

        public static LevelController LevelController => new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
