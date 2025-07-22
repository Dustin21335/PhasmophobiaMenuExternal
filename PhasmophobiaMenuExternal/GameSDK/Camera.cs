namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Camera
    {
        private IntPtr FieldOfViewPointer => Offsets.FOV;

        public float FieldOfView
        {
            get => Program.SimpleMemoryReading.Read<float>(FieldOfViewPointer);
            set => Program.SimpleMemoryReading.Write<float>(FieldOfViewPointer, value);
        }
    }
}
