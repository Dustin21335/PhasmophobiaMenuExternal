namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Camera
    {
        private IntPtr FieldOfViewPointer => Program.SimpleMemoryReading.ReadPointer(Program.UnityPlayer + 0x01C0B5C0, 0x0, 0x90, 0x20, 0x220, 0x0, 0x118, 0x180);

        public float FieldOfView
        {
            get => Program.SimpleMemoryReading.ReadFloat(FieldOfViewPointer);
            set => Program.SimpleMemoryReading.WriteFloat(FieldOfViewPointer, value);
        }
    }
}
