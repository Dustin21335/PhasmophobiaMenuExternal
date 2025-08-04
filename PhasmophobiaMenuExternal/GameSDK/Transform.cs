using PhasmophobiaMenuExternal.GameSDK.Core;
using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Transform : MemoryObject
    {
        public Transform(IntPtr address) : base(address) { }

        private async Task<IntPtr> PositionAddress()
        {
            IntPtr address = await FridaNetManager.InvokeMethod("TransformPositionAddress", Address, 0x4B1D0D0, "pointer");
            return address != IntPtr.Zero ? address : default;
        }

        public Vector3 Position
        {
            get => Program.SimpleMemoryReading.Read<Vector3>(PositionAddress().GetAwaiter().GetResult());
            set => Program.SimpleMemoryReading.Write<Vector3>(PositionAddress().GetAwaiter().GetResult(), value);
        }

        private async Task<IntPtr> RotationAddress()
        {
            IntPtr address = await FridaNetManager.InvokeMethod("TransformRotationAddress", Address, 0x4B1D240, "pointer");
            return address != IntPtr.Zero ? address : default;
        }

        public Quaternion Rotation
        {
            get => Program.SimpleMemoryReading.Read<Quaternion>(RotationAddress().GetAwaiter().GetResult());
            set => Program.SimpleMemoryReading.Write<Quaternion>(RotationAddress().GetAwaiter().GetResult(), value);
        }
    }
}
