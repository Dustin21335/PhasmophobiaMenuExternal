namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhotonPlayer 
    {
        public PhotonPlayer(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public int ActorNumber => Program.SimpleMemoryReading.Read<int>(Pointer + 0x18);

        public bool IsLocal => Program.SimpleMemoryReading.Read<bool>(Pointer + 0x01c);

        public string NickName => new mString(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20)).Value;

        public bool IsMasterClient => ActorNumber == 1;
    }
}
