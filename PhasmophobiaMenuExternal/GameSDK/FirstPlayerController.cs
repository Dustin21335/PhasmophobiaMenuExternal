namespace PhasmophobiaMenuExternal.GameSDK
{
    public class FirstPlayerController
    {
        public FirstPlayerController(IntPtr pointer)
        {
            FirstPlayerControllerPointer = pointer;
        }

        public IntPtr FirstPlayerControllerPointer;

        public float WalkSpeed         
        {
            get => Program.SimpleMemoryReading.ReadFloat(FirstPlayerControllerPointer + 0x94);
            set => Program.SimpleMemoryReading.WriteFloat(FirstPlayerControllerPointer + 0x94, value);
        }
    }
}
