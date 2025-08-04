using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class MapController : MemoryObject
    {
        public MapController(IntPtr address) : base(address) {  }

        public List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(Address + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
