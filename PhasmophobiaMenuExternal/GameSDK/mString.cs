using PhasmophobiaMenuExternal.GameSDK.Core;
using System.Text;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class mString : MemoryObject
    {
        public mString(IntPtr address) : base(address) { }

        public int Length => Program.SimpleMemoryReading.Read<int>(Address + 0x10);

        public string Value => Program.SimpleMemoryReading.ReadString(Address + 0x14, Length * 2, Encoding.Unicode);
    }
}
