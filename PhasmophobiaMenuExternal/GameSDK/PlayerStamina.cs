namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStamina
    {
        public PlayerStamina(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public float Stamina
        {
            get => (Program.SimpleMemoryReading.Read<float>(Pointer + 0x50) / 3f) * 100f;
            set => Program.SimpleMemoryReading.Write<float>(Pointer + 0x50, (value / 100f) * 3f);
        }
    }
}