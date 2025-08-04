using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class FirstPlayerController : MemoryObject
    {
        public FirstPlayerController(IntPtr address) : base(address) { }

        public float WalkSpeed         
        {
            get => Program.SimpleMemoryReading.Read<float>(Address + 0x94);
            set => Program.SimpleMemoryReading.Write<float>(Address + 0x94, value);
        }
    }
}
