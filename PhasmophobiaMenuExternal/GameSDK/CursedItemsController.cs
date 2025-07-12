namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class CursedItemsController
    {
        public static IntPtr CursedItemsControllerPointer => Program.SimpleMemoryReading.ReadPointer(Program.UnityPlayer + 0x01C966A8, 0x2E8, 0xD8, 0x10, 0xE0, 0x90, 0x118, 0x0);

        public static CursedItem OuijaBoard => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x20), "Ouija Board");

        public static CursedItem MusicBox => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x28), "Music Box");

        public static CursedItem TarotCards => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x30), "Tarot Cards");

        public static CursedItem SummoningCircle => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x38), "Summoning Circle");

        public static CursedItem HauntedMirror => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x40), "Haunted Mirror");

        public static CursedItem VoodooDoll => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x48), "Voodoo Doll");

        public static CursedItem MonkeyPaw => new CursedItem(Program.SimpleMemoryReading.ReadPointer(CursedItemsControllerPointer + 0x50), "Monkey Paw");

        public static CursedItem? CurrentCursedItem => new[] { OuijaBoard, MusicBox, TarotCards, SummoningCircle, HauntedMirror, VoodooDoll, MonkeyPaw }.FirstOrDefault(i => i.CursedItemPointer != IntPtr.Zero);
    }
}
