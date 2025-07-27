using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.GameSDK;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class GodMode : Cheat
    {
        private Dictionary<string, IntPtr> KillPlayerMethods => new Dictionary<string, IntPtr>
        {
            ["KillPlayer"] = Offsets.KillPlayer,
            ["StartKillingPlayer"] = Offsets.StartKillingPlayer,
            ["StartKillingPlayerNetworked"] = Offsets.StartKillingPlayerNetworked,
        };

        private Dictionary<string, byte[]> OriginalBytes = new Dictionary<string, byte[]>();

        public override void OnEnable()
        {
            Player localPlayer = Program.LocalPlayer;
            if (localPlayer == null) return;
            byte[] godmodePatch = BuildGodModeBytes(localPlayer.Pointer);
            KillPlayerMethods.ToList().ForEach(m =>
            {
                OriginalBytes[m.Key] = Program.SimpleMemoryReading.ReadBytes(m.Value, godmodePatch.Length);
                if (!Program.SimpleMemoryReading.WriteBytes(m.Value, godmodePatch)) Console.WriteLine($"{m.Key} failed to write bytes");
            });
        }

        public override void OnDisable()
        {
            KillPlayerMethods.Where(m => OriginalBytes.ContainsKey(m.Key)).ToList().ForEach(m =>
            {
                if (!Program.SimpleMemoryReading.WriteBytes(m.Value, OriginalBytes[m.Key])) Console.WriteLine($"{m.Key} failed to restore bytes");
            });
            OriginalBytes.Clear();
        }

        public override void OnApplicationQuit()
        {
            KillPlayerMethods.Where(m => OriginalBytes.ContainsKey(m.Key)).ToList().ForEach(m =>
            {
                if (!Program.SimpleMemoryReading.WriteBytes(m.Value, OriginalBytes[m.Key])) Console.WriteLine($"{m.Key} failed to restore bytes");
            });
            OriginalBytes.Clear();
        }

        private byte[] BuildGodModeBytes(IntPtr localPlayer)
        {
            List<byte> bytes = new List<byte>
            {
                0x48,
                0xB8
            };
            bytes.AddRange(BitConverter.GetBytes(localPlayer.ToInt64()));
            bytes.Add(0x48);
            bytes.Add(0x39);
            bytes.Add(0xC8);
            bytes.Add(0x75);
            bytes.Add(0x05);
            bytes.Add(0xC3);
            return bytes.ToArray();
        }
    }
}
