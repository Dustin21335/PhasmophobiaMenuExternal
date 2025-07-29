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

        public PhysicsCharacterController PhysicsCharacterController => new PhysicsCharacterController(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x118));

        // Localplayer only

        public Camera Camera => new Camera();

        public Vector3 LocalPlayerPosition
        {
            get => Program.SimpleMemoryReading.Read<Vector3>(Offsets.LocalPlayerPosition);
            set => Program.SimpleMemoryReading.Write<Vector3>(Offsets.LocalPlayerPosition, value);
        }
    }
}
