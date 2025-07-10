namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr GhostControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05B231A0, 0x40, 0xB8, 0x8, 0xB8, 0x10, 0x30, 0x0);

        public static GhostTraits GhostTraits => new GhostTraits(GhostControllerPointer + 0x28);

        public static LevelController LevelController = new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
