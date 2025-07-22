namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerSanity
    {
        public PlayerSanity(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20));

        public float Sanity
        {
            get => 100f - Program.SimpleMemoryReading.Read<float>(Pointer + 0x30);
            set => Program.SimpleMemoryReading.Write<float>(Pointer + 0x30, 100f - value);
        }
    }
}
