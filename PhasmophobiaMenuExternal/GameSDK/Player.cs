using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Player
    {
        public Player(IntPtr playerPointer)
        {
            PlayerPointer = playerPointer;
        }

        public IntPtr PlayerPointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0x20));

        public LevelRoom CurrentRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0x60));

        public PlayerSanity PlayerSanity => new PlayerSanity(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0xC0));

        public PlayerStamina PlayerStamina => new PlayerStamina(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0x108));

        public float SprintSpeed
        {
            get => Program.SimpleMemoryReading.ReadFloat(PlayerPointer + 0x110);
            set => Program.SimpleMemoryReading.WriteFloat(PlayerPointer + 0x110, value);
        }

        public PlayerStats PlayerStats => new PlayerStats(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0xC8));

        public FirstPlayerController FirstPlayerController => new FirstPlayerController(Program.SimpleMemoryReading.ReadPointer(PlayerPointer + 0x128));

        public Camera Camera => new Camera();

        private IntPtr PlayerXPointer => Program.SimpleMemoryReading.ReadPointer(Program.UnityPlayer + 0x01C0AF00, 0x78, 0x20, 0xF40, 0x30, 0x90, 0x70, 0x90);

        public float PlayerX
        {
            get => Program.SimpleMemoryReading.ReadFloat(PlayerXPointer);
            set => Program.SimpleMemoryReading.WriteFloat(PlayerXPointer, value);
        }

        private IntPtr PlayerYPointer => PlayerXPointer + 0x4;

        public float PlayerY
        {
            get => Program.SimpleMemoryReading.ReadFloat(PlayerYPointer);
            set => Program.SimpleMemoryReading.WriteFloat(PlayerYPointer, value);
        }

        private IntPtr PlayerZPointer => PlayerXPointer + 0x8;

        public float PlayerZ
        {
            get => Program.SimpleMemoryReading.ReadFloat(PlayerZPointer);
            set => Program.SimpleMemoryReading.WriteFloat(PlayerZPointer, value);
        }

        public Vector3 PlayerPosition
        {
            get => new Vector3(PlayerX, PlayerY, PlayerZ);
            set
            {
                PlayerX = value.X;
                PlayerY = value.Y;
                PlayerZ = value.Z;
            }
        }
    }
}
