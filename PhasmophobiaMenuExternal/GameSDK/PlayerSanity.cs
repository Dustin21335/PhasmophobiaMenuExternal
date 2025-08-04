using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerSanity : MemoryObject
    {
        public PlayerSanity(IntPtr address) : base(address) { }

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Address + 0x20));

        public float Sanity
        {
            get => 100f - Program.SimpleMemoryReading.Read<float>(Address + 0x30);
            set => Program.SimpleMemoryReading.Write<float>(Address + 0x30, 100f - value);
        }
    }
}
