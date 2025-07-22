namespace PhasmophobiaMenuExternal.GameSDK
{
    public class TextMeshProUGUI
    {
        public TextMeshProUGUI(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public string Text => new mString(Program.SimpleMemoryReading.ReadPointer(Pointer + 0xE0)).Value;
    }
}
