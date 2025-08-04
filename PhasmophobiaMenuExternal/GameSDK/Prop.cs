using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Prop : MemoryObject
    {
        public Prop(IntPtr address) : base(address) { }

        public PhotonObjectInteract PhotonObjectInteract => new PhotonObjectInteract(Program.SimpleMemoryReading.ReadPointer(Address + 0x20));
    }
}
