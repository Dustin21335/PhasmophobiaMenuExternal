using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostAI : MemoryObject
    {
        public GhostAI(IntPtr address) : base(address) { }

        public GhostInfo GhostInfo => new GhostInfo(Program.SimpleMemoryReading.ReadPointer(Address + 0x38));

        public GhostActivity GhostActivity => new GhostActivity(Program.SimpleMemoryReading.ReadPointer(Address + 0x58));

        public GhostStates GhostState => (GhostStates)Program.SimpleMemoryReading.Read<int>(Address, 0x30);

        public Player BansheeTarget => new Player(Program.SimpleMemoryReading.ReadPointer(Address + 0x110));

        public bool IsHunting => Program.SimpleMemoryReading.Read<bool>(Address + 0xF9);

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
