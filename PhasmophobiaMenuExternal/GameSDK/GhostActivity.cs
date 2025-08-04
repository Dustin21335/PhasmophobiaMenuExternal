using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostActivity : MemoryObject
    {
        public GhostActivity(IntPtr address) : base(address) { }

        public LevelController LevelController => new LevelController(Program.SimpleMemoryReading.ReadPointer(Address + 0x40));
    }
}
