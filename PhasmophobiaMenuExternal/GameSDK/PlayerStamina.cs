namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStamina
    {
        public PlayerStamina(IntPtr pointer)
        {
            PlayerStaminaPointer = pointer;
        }

        public IntPtr PlayerStaminaPointer;

        public float Stamina
        {
            get => (Program.SimpleMemoryReading.ReadFloat(PlayerStaminaPointer + 0x50) / 3f) * 100f;
            set => Program.SimpleMemoryReading.WriteFloat(PlayerStaminaPointer + 0x50, (value / 100f) * 3f);
        }
    }
}