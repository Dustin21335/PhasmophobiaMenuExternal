using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PlayerStamina : MemoryObject
    {
        public PlayerStamina(IntPtr address) : base(address) { }

        public float Stamina
        {
            get => (Program.SimpleMemoryReading.Read<float>(Address + 0x50) / 3f) * 100f;
            set => Program.SimpleMemoryReading.Write<float>(Address + 0x50, (value / 100f) * 3f);
        }
    }
}