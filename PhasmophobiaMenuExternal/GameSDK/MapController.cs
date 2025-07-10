namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class MapController
    {
        public static IntPtr MapControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05B09AB8, 0xB8, 0x0, 0x10, 0x120, 0xA8, 0x28, 0x0);

        public static GhostAI GhostAI = new GhostAI();

        public static List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(MapControllerPointer + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
