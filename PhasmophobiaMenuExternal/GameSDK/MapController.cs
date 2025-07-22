namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class MapController
    {
        public static IntPtr Pointer => Offsets.MapController;

        public static List<Player> Players => new mList(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x28)).GetPointerEntries(8).Select(e => new Player(e)).OrderByDescending(p => p.PhotonView.Owner.IsMasterClient).ToList();
    }
}
