using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class CursedItemsController : MemoryObject
    {
        public CursedItemsController(IntPtr address) : base(address) { }

        public CursedItem OuijaBoard => new CursedItem("Ouija Board", Program.SimpleMemoryReading.ReadPointer(Address + 0x20));

        public CursedItem MusicBox => new CursedItem("Music Box", Program.SimpleMemoryReading.ReadPointer(Address + 0x28));

        public CursedItem TarotCards => new CursedItem("Tarot Cards", Program.SimpleMemoryReading.ReadPointer(Address + 0x30));

        public CursedItem SummoningCircle => new CursedItem("Summoning Circle", Program.SimpleMemoryReading.ReadPointer(Address + 0x38));

        public CursedItem HauntedMirror => new CursedItem("Haunted Mirror", Program.SimpleMemoryReading.ReadPointer(Address + 0x40));

        public CursedItem VoodooDoll => new CursedItem("Voodoo Doll", Program.SimpleMemoryReading.ReadPointer(Address + 0x48));

        public CursedItem MonkeyPaw => new CursedItem("Monkey Paw", Program.SimpleMemoryReading.ReadPointer(Address + 0x50));

        public List<CursedItem> AllCursedItems => new List<CursedItem> 
        {
            OuijaBoard, MusicBox, TarotCards, SummoningCircle, HauntedMirror, VoodooDoll, MonkeyPaw
        };
    }
}
