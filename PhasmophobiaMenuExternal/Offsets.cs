using PhasmophobiaMenuExternal.Cheats;
using PhasmophobiaMenuExternal.Cheats.Core;
using SimpleMemoryReading64and32;

namespace PhasmophobiaMenuExternal
{
    public class Offsets
    {
        public static IntPtr GhostController;
        public static IntPtr MapController;
        public static IntPtr CursedItemsController;
        public static IntPtr FOV;
        public static IntPtr PlayerX;

        public static string GhostControllerPattern = "E0 57 09 ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 ?? 00 00 00 ?? 00 00 00 ?? ?? ?? ?? ?? 02 00 00 00 00 00 00 00 00 00 00 ?? 00 00 00 ?? 00 00 00 00 00 00 00 00 00 00 00 ?? 00 00 00 ?? 00 00 00 ?? 00 00 00 ?? ?? 00 00 ?? 00 00 00";
        public static string MapControllerPattern = "B0 EF F7 ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 ?? ?? ?? ?? ?? 02 00 00 ?? ?? ?? ?? ?? 02 00 00 01 00 00 00 00 00 80 3F 00 00 80 3F 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 ?? ?? ?? ?? ?? 02 00 00 01 00 00 00 00 00 00 00 00 00 00 00";
        public static string CursedItemsControllerPattern = "A0 08 60 ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? 02 00 00 ?? ?? ?? ??";
        public static string FOVPattern = "00 00 B4 42 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00";
        public static string PlayerXPattern = "?? ?? ?? 41 ?? ?? ?? 3F ?? ?? ?? 40 00 00 00 00 00 00 00 00 ?? ?? ?? ?? 00 00 00 00 ?? ?? ?? 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00 66 66 46 3F 00 00 00 00 00 00 00 00 00 00 00 80 00 00 00 80 00 00 00 80 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 00 00 00 00 00 00";


        public static IntPtr KillPlayer => Program.GameAssembly + 0xB1D860;
        public static IntPtr StartKillingPlayer => Program.GameAssembly + 0xB1DEE0;
        public static IntPtr StartKillingPlayerNetworked => Program.GameAssembly + 0xB1DE30;

        public static void UpdateOffsets()
        {
            Thread updateThread = new Thread(() =>
            {
                Console.WriteLine("Started AOB scanning");
                GhostController = Program.SimpleMemoryReading.PrivateMemoryRegions.AsParallel().Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.ReadWrite && r.State == Imports.MemoryState.Commit).SelectMany(r => Program.SimpleMemoryReading.AOBScanRegion(r, GhostControllerPattern)).Where(a =>
                {
                    int v1 = Program.SimpleMemoryReading.Read<int>(a + 0x68);
                    int v2 = Program.SimpleMemoryReading.Read<int>(a + 0xac);
                    float v3 = Program.SimpleMemoryReading.Read<float>(a + 0xb0);
                    float v4 = Program.SimpleMemoryReading.Read<float>(a + 0xa8);
                    IntPtr levelController = Program.SimpleMemoryReading.ReadPointer(a + 0x78);
                    int v5 = Program.SimpleMemoryReading.Read<int>(levelController + 0x78);
                    int v6 = Program.SimpleMemoryReading.Read<int>(levelController + 0xf0);
                    int v7 = Program.SimpleMemoryReading.Read<int>(levelController + 0xf4);
                    int v8 = Program.SimpleMemoryReading.Read<int>(levelController + 0xf8);
                    return (v1 == 0 || v1 == 1) && (v2 == 0 || v2 == 1) && v3 >= 0 && v3 < 1000 && v4 >= 0 && v4 < 1000 && levelController != IntPtr.Zero && (v5 == 0 || v5 == 1) && v6 >= 0 && v6 < 1000 && v7 >= 0 && v7 < 1000 && v8 >= 0 && v8 < 1000;
                }).FirstOrDefault();
                Console.WriteLine($"AOB Scanned Ghost Controller Address {GhostController.ToString("X")}");

                CursedItemsController = Program.SimpleMemoryReading.PrivateMemoryRegions.AsParallel().Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.ReadWrite && r.State == Imports.MemoryState.Commit).SelectMany(r => Program.SimpleMemoryReading.AOBScanRegion(r, CursedItemsControllerPattern)).Where(a =>
                {
                    return new[]
                    {
                        Program.SimpleMemoryReading.ReadPointer(a + 0x20),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x28),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x30),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x38),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x40),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x48),
                        Program.SimpleMemoryReading.ReadPointer(a + 0x50)
                }.Count(p => p != IntPtr.Zero) == 1;
                }).FirstOrDefault();
                Console.WriteLine($"AOB Scanned Cursed Items Controller Address {CursedItemsController.ToString("X")}");

                MapController = Program.SimpleMemoryReading.PrivateMemoryRegions.AsParallel().Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.ReadWrite && r.State == Imports.MemoryState.Commit).SelectMany(r => Program.SimpleMemoryReading.AOBScanRegion(r, MapControllerPattern)).Where(a =>
                {
                    int v1 = Program.SimpleMemoryReading.Read<int>(a + 0x38);
                    float v2 = Program.SimpleMemoryReading.Read<float>(a + 0x3c);
                    float v3 = Program.SimpleMemoryReading.Read<float>(a + 0x40);
                    return v1 >= 0 && v1 < 100 && v2 >= 0 && v2 < 100 && v3 >= 0 && v3 < 100;
                }).FirstOrDefault();
                Console.WriteLine($"AOB Scanned Map Controller Address {MapController.ToString("X")}");

                FOV = Program.SimpleMemoryReading.PrivateMemoryRegions.AsParallel().Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.NoAccess && r.State == Imports.MemoryState.Commit).SelectMany(r => Program.SimpleMemoryReading.AOBScanRegion(r, FOVPattern)).Where(a =>
                {
                    float v = Program.SimpleMemoryReading.Read<float>(a);
                    return v >= 10 && v < 180;
                }).FirstOrDefault();
                Console.WriteLine($"AOB Scanned FOV Address {FOV.ToString("X")}");

                PlayerX = Program.SimpleMemoryReading.PrivateMemoryRegions.AsParallel().SelectMany(r => Program.SimpleMemoryReading.AOBScanRegion(r, PlayerXPattern)).Where(a =>
                {
                    float v1 = Program.SimpleMemoryReading.Read<float>(a);
                    float v2 = Program.SimpleMemoryReading.Read<float>(a + 0x4);
                    float v3 = Program.SimpleMemoryReading.Read<float>(a + 0x8);
                    return v1 >= -10000 && v1 <= 10000 && v2 >= -10000 && v2 <= 10000 && v3 >= -10000 && v3 <= 10000;
                }).FirstOrDefault();
                Console.WriteLine($"AOB Scanned Player X Address {PlayerX.ToString("X")}");

                Console.WriteLine("Completed AOB scanning");

                Console.WriteLine($"Kill Player Address {KillPlayer.ToString("X")}");
                Console.WriteLine($"Start Killing Player Address {StartKillingPlayer.ToString("X")}");
                Console.WriteLine($"Start Killing Player Networked Address {StartKillingPlayerNetworked.ToString("X")}");

                if (Cheat.Instance<GodMode>().Enabled)
                {
                    Cheat.Instance<GodMode>().OnDisable();
                    Cheat.Instance<GodMode>().OnEnable();
                }
            });
            updateThread.IsBackground = true;
            updateThread.Priority = ThreadPriority.Highest;
            updateThread.Start();
        }
    }
}
