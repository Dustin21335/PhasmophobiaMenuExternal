namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonObjectInteract
    {
        public PhotonObjectInteract(nint pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20));
    }
}
