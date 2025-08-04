using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class JournalController : MemoryObject
    {
        public JournalController(IntPtr address) : base(address) { }

        public PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Address + 0xF0));

        public TextMeshProUGUI GhostName => new TextMeshProUGUI(Program.SimpleMemoryReading.ReadPointer(Address + 0x110));
    }
}
