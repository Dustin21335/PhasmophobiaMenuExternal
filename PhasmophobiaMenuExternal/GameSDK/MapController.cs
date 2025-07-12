namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class MapController
    {
        public static IntPtr MapControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.UnityPlayer + 0x01D0CD38, 0x168, 0x120, 0x1E8, 0x0, 0x68, 0x60, 0x100);

        public static List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(MapControllerPointer + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
