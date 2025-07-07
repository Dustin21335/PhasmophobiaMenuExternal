namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Prop
    {
        public Prop(IntPtr pointer)
        {
            PropPointer = pointer;
        }

        public IntPtr PropPointer;

        public PhotonObjectInteract PhotonObjectInteract => new PhotonObjectInteract(Program.SimpleMemoryReading.ReadPointer(PropPointer + 0x20));
    }
}
