namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Equipment
    {
        public Equipment(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public Prop Prop => new Prop(Pointer);
    }
}
