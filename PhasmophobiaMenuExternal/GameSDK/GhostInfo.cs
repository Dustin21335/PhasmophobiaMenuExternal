using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostInfo : MemoryObject
    {
        public GhostInfo(IntPtr address) : base(address) { }

        public GhostTraits GhostTraits => new GhostTraits(Address + 0x28);

        public LevelRoom FavoriteRoom => new LevelRoom(Program.SimpleMemoryReading.ReadPointer(Address + 0x70));
    }
}
