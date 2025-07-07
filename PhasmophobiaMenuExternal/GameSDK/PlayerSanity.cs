namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerSanity
    {
        public PlayerSanity(IntPtr pointer)
        {
            PlayerSanityPointer = pointer;
        }

        public IntPtr PlayerSanityPointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(PlayerSanityPointer + 0x20));

        public float Sanity
        {
            get => 100f - Program.SimpleMemoryReading.ReadFloat(PlayerSanityPointer + 0x30);
            set => Program.SimpleMemoryReading.WriteFloat(PlayerSanityPointer + 0x30, 100f - value);
        }
    }
}
