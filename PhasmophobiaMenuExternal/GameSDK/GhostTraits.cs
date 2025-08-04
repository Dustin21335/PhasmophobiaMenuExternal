using PhasmophobiaMenuExternal.GameSDK.Core;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostTraits : MemoryObject
    {
        public GhostTraits(IntPtr address) : base(address) { }

        public int Age => Program.SimpleMemoryReading.Read<int>(Address + 0x18);

        public bool IsMale => Program.SimpleMemoryReading.Read<bool>(Address + 0x1C);

        public bool IsShy => Program.SimpleMemoryReading.Read<bool>(Address + 0x30);

        public GhostTypes GhostType => (GhostTypes)Program.SimpleMemoryReading.Read<int>(Address + 0x0);

        public GhostTypes MimicGhostType => (GhostTypes)Program.SimpleMemoryReading.Read<int>(Address + 0x4);

        public List<Evidences> AllPossibleEvidence => new mList(Program.SimpleMemoryReading.ReadPointer(Address + 0x8)).GetEntries(4).Select(e => (Evidences)Program.SimpleMemoryReading.Read<int>(e)).ToList();

        public List<Evidences> AllEvidence => new mList(Program.SimpleMemoryReading.ReadPointer(Address + 0x10)).GetEntries(4).Select(e => (Evidences)Program.SimpleMemoryReading.Read<int>(e)).ToList();

        public enum GhostTypes
        {
            Spirit = 0,
            Wraith = 1,
            Phantom = 2,
            Poltergeist = 3,
            Banshee = 4,
            Jinn = 5,
            Mare = 6,
            Revenant = 7,
            Shade = 8,
            Demon = 9,
            Yurei = 10,
            Oni = 11,
            Yokai = 12,
            Hantu = 13,
            Goryo = 14,
            Myling = 15,
            Onryo = 16,
            TheTwins = 17,
            Raiju = 18,
            Obake = 19,
            Mimic = 20,
            Moroi = 21,
            Deogen = 22,
            Thaye = 23,
            None = 24
        }

        public enum Evidences
        {
            None = 0,
            EMF = 1,
            SpiritBox = 2,
            UltraViolet = 3,
            GhostOrb = 4,
            GhostWritingBook = 5,
            Temperature = 6,
            DotsProjector = 7
        }
    }
}
