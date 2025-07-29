using SimpleMemoryReading64and32;
using System.Numerics;

namespace PhasmophobiaMenuExternal
{
    public class Offsets
    {
        public static IntPtr GhostAI;
        public static IntPtr CursedItemsController;
        public static IntPtr MapController;

        public static IntPtr FOV;
        public static IntPtr LocalPlayerPosition;

        public static string FOVPattern = "?? ?? B4 42 ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F";
        public static string LocalPlayerPositionPattern = "?? ?? ?? 41 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? 66 66 46 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 ?? ?? ?? 80 ?? ?? ?? 80 ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F";

        public static void Initialize()
        {
            Hooks.HookTriggered += (sender, hookMessage) =>
            {
                Console.WriteLine($"{hookMessage.Name} address {hookMessage.Address}");
                switch (hookMessage.Name)
                {
                    case "GhostAIStart":
                        GhostAI = new IntPtr(Convert.ToInt64(hookMessage.Address, 16));
                        Update();
                        break;
                    case "CursedItemsControllerStart":
                        CursedItemsController = new IntPtr(Convert.ToInt64(hookMessage.Address, 16));
                        break;
                    case "MapControllerStart":
                        MapController = new IntPtr(Convert.ToInt64(hookMessage.Address, 16));
                        break;
                }
            };
        }

        public static void Update()
        {
            Thread offsetsUpdateThread = new Thread(() =>
            {
                FOV = IntPtr.Zero;
                LocalPlayerPosition = IntPtr.Zero;

                Console.WriteLine("AOB scanning started");

                foreach (Imports.Region region in Program.SimpleMemoryReading.PrivateMemoryRegions.Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.NoAccess && r.State == Imports.MemoryState.Commit))
                {
                    if (FOV == IntPtr.Zero)
                    {
                        FOV = Program.SimpleMemoryReading.AOBScanRegion(region, FOVPattern).Where(a =>
                        {
                            float value = Program.SimpleMemoryReading.Read<float>(a);
                            return value >= 10 && value < 180;
                        }).FirstOrDefault();
                        if (FOV != IntPtr.Zero) Console.WriteLine($"AOB scanned FOV address {FOV.ToString("X")}");
                    }

                    if (LocalPlayerPosition == IntPtr.Zero)
                    {
                        LocalPlayerPosition = Program.SimpleMemoryReading.AOBScanRegion(region, LocalPlayerPositionPattern).Where(a =>
                        {
                            Vector3 value = Program.SimpleMemoryReading.Read<Vector3>(a);
                            return Math.Abs(value.X) <= 10000 && Math.Abs(value.Y) <= 10000 && Math.Abs(value.Z) <= 10000;
                        }).FirstOrDefault();
                        if (LocalPlayerPosition != IntPtr.Zero) Console.WriteLine($"AOB scanned Local Player Position address {LocalPlayerPosition.ToString("X")}");
                    }

                    if (FOV != IntPtr.Zero && LocalPlayerPosition != IntPtr.Zero) break;
                }

                Console.WriteLine("Completed AOB scanning");
            });
            offsetsUpdateThread.IsBackground = true;
            offsetsUpdateThread.Priority = ThreadPriority.Highest;
            offsetsUpdateThread.Start();
        }
    }
}
