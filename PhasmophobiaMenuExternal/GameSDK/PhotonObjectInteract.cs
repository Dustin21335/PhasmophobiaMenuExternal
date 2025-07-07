namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonObjectInteract
    {
        public PhotonObjectInteract(nint pointer)
        {
            PhotonObjectInteractPointer = pointer;
        }

        public IntPtr PhotonObjectInteractPointer;

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(PhotonObjectInteractPointer + 0x20));

        public PhotonTransformView PhotonTransformView => new PhotonTransformView(Program.SimpleMemoryReading.ReadPointer(PhotonObjectInteractPointer + 0x28));
    }
}
