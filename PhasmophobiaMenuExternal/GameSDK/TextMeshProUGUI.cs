using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class TextMeshProUGUI : MemoryObject
    {
        public TextMeshProUGUI(IntPtr address) : base(address) { }

        public string Text => new mString(Program.SimpleMemoryReading.ReadPointer(Address + 0xE0)).Value;
    }
}
