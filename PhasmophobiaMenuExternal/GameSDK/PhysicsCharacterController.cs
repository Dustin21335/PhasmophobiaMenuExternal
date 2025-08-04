using PhasmophobiaMenuExternal.GameSDK.Core;
using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhysicsCharacterController : MemoryObject
    {
        public PhysicsCharacterController(IntPtr address) : base(address) { }

        public Vector3 Position => Program.SimpleMemoryReading.Read<Vector3>(Address + 0xC8);
    }
}
