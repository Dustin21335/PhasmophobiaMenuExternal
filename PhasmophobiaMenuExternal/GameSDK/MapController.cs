namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class MapController
    {
        public static IntPtr MapControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05B2E460, 0x48, 0x40, 0x90, 0x40, 0xB8, 0x20, 0x0);

        public static GhostAI GhostAI = new GhostAI();

        public static List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(MapControllerPointer + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
