namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostController
    {
        public static IntPtr Pointer => Offsets.GhostController;

        public static GhostTraits GhostTraits => new GhostTraits(Pointer + 0x28);

        public static LevelController LevelController => new LevelController();

        public static GhostAI GhostAI => new GhostAI();
    }
}
