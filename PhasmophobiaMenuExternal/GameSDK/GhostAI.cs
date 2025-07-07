namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostAI
    {
        public IntPtr GhostAIPointer => Program.SimpleMemoryReading.ReadPointer(GhostController.GhostControllerPointer + 0x90);

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

        public GhostInfo GhostInfo => new GhostInfo();
        public GhostStates GhostState => (GhostStates)Program.SimpleMemoryReading.ReadInt(GhostAIPointer + 0x30);
        public bool IsHunting => GhostState == GhostStates.Hunting;
    }
}
