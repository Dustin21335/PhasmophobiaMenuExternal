using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonNetwork : MemoryObject
    {
        public PhotonNetwork(IntPtr address) : base(address) { }

        public enum RpcTarget
        {
            All = 0,
            Others = 1,
            MasterClient = 2,
            AllBuffered = 3,
            OthersBuffered = 4,
            AllViaServer = 5,
            AllBufferedViaServer = 6
        }
    }
}
