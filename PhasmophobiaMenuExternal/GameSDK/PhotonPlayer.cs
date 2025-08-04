using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonPlayer : MemoryObject
    {
        public PhotonPlayer(IntPtr address) : base(address) { }

        public int ActorNumber => Program.SimpleMemoryReading.Read<int>(Address + 0x18);

        public bool IsLocal => Program.SimpleMemoryReading.Read<bool>(Address + 0x01c);

        public string NickName => new mString(Program.SimpleMemoryReading.ReadPointer(Address + 0x20)).Value;

        public bool IsMasterClient => ActorNumber == 1;
    }
}
