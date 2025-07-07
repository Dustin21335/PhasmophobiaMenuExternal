namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonPlayer 
    {
        public PhotonPlayer(IntPtr pointer)
        {
            PhotonPlayerPointer = pointer;
        }

        public IntPtr PhotonPlayerPointer;

        public int ActorNumber => Program.SimpleMemoryReading.ReadInt(PhotonPlayerPointer + 0x18);

        public bool IsLocal => Program.SimpleMemoryReading.ReadBool(PhotonPlayerPointer + 0x01c);

        public string NickName => new mString(Program.SimpleMemoryReading.ReadPointer(PhotonPlayerPointer + 0x20)).Value;

        public bool IsMasterClient => ActorNumber == 1;
    }
}
