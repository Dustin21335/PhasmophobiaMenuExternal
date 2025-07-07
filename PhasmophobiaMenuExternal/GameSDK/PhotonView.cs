namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonView
    {
        public PhotonView(IntPtr pointer)
        {
            PhotonViewPointer = pointer;
        }

        public IntPtr PhotonViewPointer;

        public bool IsMine => Program.SimpleMemoryReading.ReadBool(PhotonViewPointer + 0x68);
        public PhotonPlayer Owner => new PhotonPlayer(Program.SimpleMemoryReading.ReadPointer(PhotonViewPointer + 0x80));
    }
}
