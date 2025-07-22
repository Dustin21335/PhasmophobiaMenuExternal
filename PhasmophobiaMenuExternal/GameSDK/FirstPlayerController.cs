namespace PhasmophobiaMenuExternal.GameSDK
{
    public class FirstPlayerController
    {
        public FirstPlayerController(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public float WalkSpeed         
        {
            get => Program.SimpleMemoryReading.Read<float>(Pointer + 0x94);
            set => Program.SimpleMemoryReading.Write<float>(Pointer + 0x94, value);
        }
    }
}
