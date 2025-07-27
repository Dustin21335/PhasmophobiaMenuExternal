using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Player
    {
        public Player(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20));

        public LevelRoom CurrentRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x60));

        public PlayerSanity PlayerSanity => new PlayerSanity(Program.SimpleMemoryReading.ReadPointer(Pointer + 0xC0));

        public PlayerStamina PlayerStamina => new PlayerStamina(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x108));

        public float SprintSpeed
        {
            get => Program.SimpleMemoryReading.Read<float>(Pointer + 0x110);
            set => Program.SimpleMemoryReading.Write<float>(Pointer + 0x110, value);
        }

        public PlayerStats PlayerStats => new PlayerStats(Program.SimpleMemoryReading.ReadPointer(Pointer + 0xC8));

        public FirstPlayerController FirstPlayerController => new FirstPlayerController(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x128));

        public Camera Camera => new Camera();

        private IntPtr PlayerXPointer => Offsets.PlayerX;

        public float PlayerX
        {
            get => Program.SimpleMemoryReading.Read<float>(PlayerXPointer);
            set => Program.SimpleMemoryReading.Write<float>(PlayerXPointer, value);
        }

        private IntPtr PlayerYPointer => PlayerXPointer + 0x4;

        public float PlayerY
        {
            get => Program.SimpleMemoryReading.Read<float>(PlayerYPointer);
            set => Program.SimpleMemoryReading.Write<float>(PlayerYPointer, value);
        }

        private IntPtr PlayerZPointer => PlayerXPointer + 0x8;

        public float PlayerZ
        {
            get => Program.SimpleMemoryReading.Read<float>(PlayerZPointer);
            set => Program.SimpleMemoryReading.Write<float>(PlayerZPointer, value);
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
