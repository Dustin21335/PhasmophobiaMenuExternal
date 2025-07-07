namespace PhasmophobiaMenuExternal.GameSDK
{
    public class CursedItem
    {
        public CursedItem(IntPtr pointer, string name)
        {
            CursedItemPointer = pointer;
            CursedItemName = name;    
        }

        public IntPtr CursedItemPointer;

        public string CursedItemName;

        public Equipment Equipment => new Equipment(CursedItemPointer);
    }
}
