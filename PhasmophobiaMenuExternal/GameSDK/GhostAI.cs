namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class GhostAI
    {
        public static IntPtr Pointer => Offsets.GhostAI;

        public static GhostInfo GhostInfo => new GhostInfo();

        public static GhostModel GhostModel => new GhostModel();

        public static GhostStates GhostState => (GhostStates)Program.SimpleMemoryReading.Read<int>(Pointer, 0x30);

        public static Player BansheeTarget => new Player(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x110));

        public static bool IsHunting => Program.SimpleMemoryReading.Read<bool>(Pointer + 0xF9);

        public enum GhostStates
        {
            Idle = 0,
            Wander = 1,
            Hunting = 2,
            FavouriteRoom = 3,
            Light = 4,
            Door = 5,
            Throwing = 6,
            Fusebox = 7,
            Appear = 8,
            DoorKnock = 9,
            WindowKnock = 10,
            CarAlarm = 11,
            Flicker = 12,
            CCTV = 13,
            RandomEvent = 14,
            GhostAbility = 15,
            Mannequin = 16,
            TeleportObject = 17,
            Interact = 18,
            SummoningCircle = 19,
            MusicBox = 20,
            Dots = 21,
            Salt = 22,
            Ignite = 23
        }
    }
}
