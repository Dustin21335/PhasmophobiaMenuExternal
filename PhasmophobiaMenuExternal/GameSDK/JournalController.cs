namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class JournalController
    {
        public static IntPtr JournalControllerPointer => Program.LocalPlayer != null ? Program.SimpleMemoryReading.ReadPointer(Program.LocalPlayer.PlayerPointer + 0xD8) : IntPtr.Zero;
  
        public static PhotonView PhotonView => new PhotonView(Program.SimpleMemoryReading.ReadPointer(JournalControllerPointer + 0xF0));

        public static TextMeshProUGUI GhostName => new TextMeshProUGUI(Program.SimpleMemoryReading.ReadPointer(JournalControllerPointer + 0x110));
    }
}
