using System.Text;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class mString
    {
        public mString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public int Length => Program.SimpleMemoryReading.Read<int>(Pointer + 0x10);

        public string Value => Program.SimpleMemoryReading.ReadString(Pointer + 0x14, Length * 2, Encoding.Unicode);
    }
}
