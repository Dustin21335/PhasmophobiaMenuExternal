using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class mList : MemoryObject
    {
        public mList(IntPtr address) : base(address) { }

        public IntPtr Items => Program.SimpleMemoryReading.ReadPointer(Address + 0x10);

        public int Count => Program.SimpleMemoryReading.Read<int>(Address + 0x18);

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
