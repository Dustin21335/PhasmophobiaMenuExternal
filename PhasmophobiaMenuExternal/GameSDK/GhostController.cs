namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr GhostControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.UnityPlayer + 0x01C966A8, 0x1E0, 0x30, 0x140, 0x18, 0xC0, 0x60, 0x0);

        public static GhostTraits GhostTraits => new GhostTraits(GhostControllerPointer + 0x28);

        public static LevelController LevelController => new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
