namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class JournalController
    {
        public static IntPtr Pointer => Program.LocalPlayer != null ? Program.SimpleMemoryReading.ReadPointer(Program.LocalPlayer.Pointer + 0xD8) : IntPtr.Zero;
  
        public static PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(Pointer + 0xF0));

        public static TextMeshProUGUI GhostName => new TextMeshProUGUI(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x110));
    }
}
