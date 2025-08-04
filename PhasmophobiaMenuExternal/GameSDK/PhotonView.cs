using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonView : MemoryObject
    {
        public PhotonView(IntPtr address) : base(address) { }

        public bool IsMine => Program.SimpleMemoryReading.Read<bool>(Address + 0x68);

        public PhotonPlayer Owner => new PhotonPlayer(Program.SimpleMemoryReading.ReadPointer(Address + 0x80));
    }
}
