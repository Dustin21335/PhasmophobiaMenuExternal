namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonView
    {
        public PhotonView(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public bool IsMine => Program.SimpleMemoryReading.Read<bool>(Pointer + 0x68);
        public PhotonPlayer Owner => new PhotonPlayer(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x80));
    }
}
