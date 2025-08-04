using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonObjectInteract : MemoryObject
    {
        public PhotonObjectInteract(IntPtr address) : base(address) { }

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Address + 0x20));
    }
}
