﻿namespace PhasmophobiaMenuExternal.GameSDK
{
    public static class CursedItemsController
    {
        public static IntPtr Pointer => Offsets.CursedItemsController;

        public static CursedItem OuijaBoard => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x20), "Ouija Board");

        public static CursedItem MusicBox => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x28), "Music Box");

        public static CursedItem TarotCards => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x30), "Tarot Cards");

        public static CursedItem SummoningCircle => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x38), "Summoning Circle");

        public static CursedItem HauntedMirror => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x40), "Haunted Mirror");

        public static CursedItem VoodooDoll => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x48), "Voodoo Doll");

        public static CursedItem MonkeyPaw => new CursedItem(Program.SimpleMemoryReading.ReadPointer(Pointer + 0x50), "Monkey Paw");

        public static CursedItem? CurrentCursedItem => new[] { OuijaBoard, MusicBox, TarotCards, SummoningCircle, HauntedMirror, VoodooDoll, MonkeyPaw }.FirstOrDefault(i => i.Pointer != IntPtr.Zero);
    }
}
