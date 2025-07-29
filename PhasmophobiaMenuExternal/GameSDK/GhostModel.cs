using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class GhostModel
    {
        public IntPtr Pointer => Program.SimpleMemoryReading.ReadPointer(GhostAI.Pointer + 0x38);

        public Vector3 Position => Program.SimpleMemoryReading.Read<Vector3>(Pointer + 0x50);
    }
}
