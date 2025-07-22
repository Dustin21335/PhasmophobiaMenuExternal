namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Prop
    {
        public Prop(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public PhotonObjectInteract PhotonObjectInteract => new PhotonObjectInteract(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20));
    }
}
