using PhasmophobiaMenuExternal.GameSDK.Core;
using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Player : MemoryObject
    {
        public Player(IntPtr address) : base(address) { }

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Address + 0x20));

        public LevelRoom CurrentRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Address + 0x60));

        public PlayerSanity PlayerSanity => new PlayerSanity(Program.SimpleMemoryReading.ReadPointer(Address + 0xC0));

        public PlayerStamina PlayerStamina => new PlayerStamina(Program.SimpleMemoryReading.ReadPointer(Address + 0x108));

        public float SprintSpeed
        {
            get => Program.SimpleMemoryReading.Read<float>(Address + 0x110);
            set => Program.SimpleMemoryReading.Write<float>(Address + 0x110, value);
        }

        public PlayerStats PlayerStats => new PlayerStats(Program.SimpleMemoryReading.ReadPointer(Address + 0xC8));

        public FirstPlayerController FirstPlayerController => new FirstPlayerController(Program.SimpleMemoryReading.ReadPointer(Address + 0x128));

        public PhysicsCharacterController PhysicsCharacterController => new PhysicsCharacterController(Program.SimpleMemoryReading.ReadPointer(Address + 0x118));

        public JournalController JournalController => new JournalController(Program.SimpleMemoryReading.ReadPointer(Address + 0xD8));

        public PlayerCharacter PlayerCharacter => new PlayerCharacter(Program.SimpleMemoryReading.ReadPointer(Address + 0x30));

        public PCPropGrab PCPropGrab => new PCPropGrab(Program.SimpleMemoryReading.ReadPointer(Address + 0x130));

        public bool IsDead => Program.SimpleMemoryReading.Read<bool>(Address + 0x29);

        public Transform Transform => new Transform(Program.SimpleMemoryReading.ReadPointer(Address + 0x68));

        // Local Player Only    

        public float FieldOfView
        {
            get => Program.SimpleMemoryReading.Read<float>(GameObjectManager.FOV);
            set => Program.SimpleMemoryReading.Write<float>(GameObjectManager.FOV, value);
        }

        public Vector3 LocalPlayerPosition
        {
            get => Program.SimpleMemoryReading.Read<Vector3>(GameObjectManager.LocalPlayerPosition);
            set => Program.SimpleMemoryReading.Write<Vector3>(GameObjectManager.LocalPlayerPosition, value);
        }
    }
}
