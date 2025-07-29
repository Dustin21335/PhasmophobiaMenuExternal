using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class PhysicsCharacterController
    {
        public PhysicsCharacterController(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public Vector3 Position => Program.SimpleMemoryReading.Read<Vector3>(Pointer + 0xC8);
    }
}
