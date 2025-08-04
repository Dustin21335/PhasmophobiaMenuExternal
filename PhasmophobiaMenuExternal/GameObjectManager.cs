using PhasmophobiaMenuExternal.GameSDK;
using SimpleMemoryReading64and32;
using System.Numerics;

namespace PhasmophobiaMenuExternal
{
    public static class GameObjectManager
    {
        public static Player? LocalPlayer => Players?.FirstOrDefault(p => p.PhotonView.Owner.IsLocal);
        public static List<Player> Players => MapController?.Players;
        public static List<CursedItem> CursedItems => CursedItemsController?.AllCursedItems.Where(c => c.Address != IntPtr.Zero).ToList();

        public static GhostAI GhostAI;
        public static MapController MapController;
        public static CursedItemsController CursedItemsController;


        public static IntPtr FOV;
        public static IntPtr LocalPlayerPosition;

        public static string FOVPattern = "?? ?? B4 42 ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 3F";
        public static string LocalPlayerPositionPattern = "?? ?? ?? 41 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? ?? ?? ?? ?? ?? ?? 66 66 46 3F ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 80 ?? ?? ?? 80 ?? ?? ?? 80 ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F ?? ?? 80 3F";

        public static void Initialize()
        {
            FridaNetManager.HookMethod("GhostAIStart", 0x1CD31F0);
            FridaNetManager.HookMethod("CursedItemsControllerAwake", 0x1F57640);
            FridaNetManager.HookMethod("MapControllerAwake", 0x201B130);
            FridaNetManager.HookMethod("MainManagerAwake", 0x8C9900);

            FridaNetManager.OnHookMessageTriggered += (sender, hookmessage) =>
            {
                IntPtr address = new IntPtr(Convert.ToInt64(hookmessage.Address, 16));
                switch (hookmessage.Name)
                {
                    case "GhostAIStart":
                        GhostAI = new GhostAI(address);
                        UpdatePatternAddresses();
                        break;
                    case "CursedItemsControllerAwake":
                        CursedItemsController = new CursedItemsController(address);
                        break;
                    case "MapControllerAwake":
                        MapController = new MapController(address);
                        break;
                    case "MainManagerAwake":
                        Clear();
                        break;
                }
            };
        }

        public static void Clear()
        {
            GhostAI = null;
            Players?.Clear();
            CursedItems?.Clear();
            FOV = IntPtr.Zero;
            LocalPlayerPosition = IntPtr.Zero;
        }

        public static void UpdatePatternAddresses()
        {
            Thread offsetsUpdateThread = new Thread(() =>
            {
                foreach (Imports.Region region in Program.SimpleMemoryReading.PrivateMemoryRegions.Where(r => r.Protect == Imports.MemoryProtect.ReadWrite && r.AllocationProtect == Imports.MemoryProtect.NoAccess && r.State == Imports.MemoryState.Commit))
                {
                    if (FOV == IntPtr.Zero)
                    {
                        FOV = Program.SimpleMemoryReading.AOBScanRegion(region, FOVPattern).Where(a =>
                        {
                            float value = Program.SimpleMemoryReading.Read<float>(a);
                            return value >= 10 && value < 180;
                        }).FirstOrDefault();
                    }

                    if (LocalPlayerPosition == IntPtr.Zero)
                    {
                        LocalPlayerPosition = Program.SimpleMemoryReading.AOBScanRegion(region, LocalPlayerPositionPattern).Where(a =>
                        {
                            Vector3 value = Program.SimpleMemoryReading.Read<Vector3>(a);
                            return Math.Abs(value.X) <= 10000 && Math.Abs(value.Y) <= 10000 && Math.Abs(value.Z) <= 10000;
                        }).FirstOrDefault();         
                    }

                    if (FOV != IntPtr.Zero && LocalPlayerPosition != IntPtr.Zero) break;
                }
            });
            offsetsUpdateThread.IsBackground = true;
            offsetsUpdateThread.Priority = ThreadPriority.Highest;
            offsetsUpdateThread.Start();
        }
    }
}
