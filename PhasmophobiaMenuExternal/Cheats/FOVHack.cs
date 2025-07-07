using PhasmophobiaMenuExternal.Cheats.Core;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class FOVHack : TaskCheat
    {
        public FOVHack() 
        {
            Value = 90f;
        }

        private IntPtr FOVPointer => Program.SimpleMemoryReading.ReadPointer(Program.GameAssembly + 0x05A47EE8, 0xC0, 0xB8, 0x8, 0x20, 0x78, 0x10, 0x180);

        public float OriginalFOV = 1f;

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
