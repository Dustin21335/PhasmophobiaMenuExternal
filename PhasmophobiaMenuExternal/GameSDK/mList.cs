namespace PhasmophobiaMenuExternal.GameSDK
{
    public class mList
    {
        public mList(IntPtr pointer)
        {
            Pointer = pointer;
        }   

        public IntPtr Pointer;

        public IntPtr Items => Program.SimpleMemoryReading.ReadPointer(Pointer + 0x10);

        public int Count => Program.SimpleMemoryReading.ReadInt(Pointer + 0x18);

        public List<IntPtr> GetEntries(int size)
        {
            List<IntPtr> entries = new List<IntPtr>();
            IntPtr baseAddress = IntPtr.Add(Items, 0x20);
            for (int i = 0; i < Count; i++) entries.Add(IntPtr.Add(baseAddress, i * size));
            return entries;
        }

        public List<IntPtr> GetPointerEntries(int pointerSize)
        {
            List<IntPtr> entries = new List<IntPtr>();
            IntPtr baseAddress = IntPtr.Add(Items, 0x20);
            for (int i = 0; i < Count; i++) entries.Add(Program.SimpleMemoryReading.ReadPointer(IntPtr.Add(baseAddress, i * pointerSize)));
            return entries;
        }
    }
}
