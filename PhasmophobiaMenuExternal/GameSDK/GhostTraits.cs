namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostTraits
    {
        public GhostTraits(IntPtr pointer)
        {
            GhostTraitsPointer = pointer;
        }

        public IntPtr GhostTraitsPointer;

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

        public enum Evidence
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

        public GhostTypes GhostType => (GhostTypes)Program.SimpleMemoryReading.Read<int>(GhostTraitsPointer + 0x0);

        public List<Evidence> Evidences => new mList(Program.SimpleMemoryReading.ReadPointer(GhostTraitsPointer + 0x8)).GetEntries(4).Select(e => (Evidence)Program.SimpleMemoryReading.Read<int>(e)).ToList();
    }
}
