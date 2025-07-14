namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class MapController
    {
        public static IntPtr MapControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05E64D48, 0xC78, 0x890, 0x20, 0xB8, 0x10, 0x0);

        public static List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(MapControllerPointer + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
