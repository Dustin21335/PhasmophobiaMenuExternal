namespace PhasmophobiaMenuExternal.GameSDK
{
    public class CursedItem
    {
        public CursedItem(IntPtr pointer, string name)
        {
            Pointer = pointer;
            Name = name;    
        }

        public IntPtr Pointer;

        public string Name;

        public Equipment Equipment => new Equipment(Pointer);
    }
}
