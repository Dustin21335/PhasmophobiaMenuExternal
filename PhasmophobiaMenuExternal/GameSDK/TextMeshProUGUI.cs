namespace PhasmophobiaMenuExternal.GameSDK
{
    public class TextMeshProUGUI
    {
        public TextMeshProUGUI(IntPtr pointer)
        {
            TextMeshProUGUIPointer = pointer;
        }

        public IntPtr TextMeshProUGUIPointer;

        public string Text => new mString(Program.SimpleMemoryReading.ReadPointer(TextMeshProUGUIPointer + 0xE0)).Value;
    }
}
