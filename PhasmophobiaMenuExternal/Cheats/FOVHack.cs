using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class FOVHack : TaskCheat
    {
        public FOVHack() 
        {
            Value = 90f;
        }

        private IntPtr FOVPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05B4E908, 0x3D0, 0x20, 0x60, 0x38, 0x58, 0x10, 0x180);

        private float OriginalFOV = 1f;

        public override void OnEnable()
        {
            OriginalFOV = Program.SimpleMemoryReading.ReadFloat(FOVPointer);
        }

        public override void OnDisable()
        {
            if (Program.SimpleMemoryReading.ReadFloat(FOVPointer) != OriginalFOV) Program.SimpleMemoryReading.WriteFloat(FOVPointer, OriginalFOV);
            OriginalFOV = -1f;
        }

        public override Task Update()
        {
            if (OriginalFOV != -1f && Program.SimpleMemoryReading.ReadFloat(FOVPointer) != Value) Program.SimpleMemoryReading.WriteFloat(FOVPointer, Value);
            return Task.CompletedTask;
        }
    }
}
