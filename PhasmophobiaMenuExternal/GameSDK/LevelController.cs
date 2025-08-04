using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelController : MemoryObject
    {
        public LevelController(IntPtr address) : base(address) { }

        public LevelRoom GhostCurrentRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Address + 0x38));
    }
}
