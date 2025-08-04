using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Key : MemoryObject
    {
        public Key(IntPtr address) : base(address) { }

        public PhotonObjectInteract PhotonObjectInteract => new PhotonObjectInteract(Program.SimpleMemoryReading.ReadPointer(Address + 0x28));
    }
}
