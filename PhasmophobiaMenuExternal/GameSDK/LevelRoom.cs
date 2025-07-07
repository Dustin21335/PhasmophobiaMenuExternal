namespace PhasmophobiaMenuExternal.GameSDK
{
    public class LevelRoom
    {
        public LevelRoom(IntPtr pointer)
        {
            LevelRoomPointer = pointer;
        }

        public IntPtr LevelRoomPointer;

        public string RoomName => new mString(Program.SimpleMemoryReading.ReadPointer(LevelRoomPointer + 0x60)).Value;

        public int TimePlayerBeenInRoom => Program.SimpleMemoryReading.ReadInt(LevelRoomPointer + 0x74);

        public bool IsBasementOrAttic => Program.SimpleMemoryReading.ReadBool(LevelRoomPointer + 0x78);
    }
}
