namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Camera
    {
        private IntPtr FieldOfViewPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05BD62C0, 0x20, 0xB8, 0x10, 0x28, 0x58, 0x10, 0x180);

        public float FieldOfView
        {
            get => Program.SimpleMemoryReading.ReadFloat(FieldOfViewPointer);
            set => Program.SimpleMemoryReading.WriteFloat(FieldOfViewPointer, value);
        }
    }
}
