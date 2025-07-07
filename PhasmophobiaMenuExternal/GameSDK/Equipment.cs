using System.Numerics;

namespace PhasmophobiaMenuExternal.GameSDK
{
    public class Equipment
    {
        public Equipment(IntPtr pointer)
        {
            EquipmentPointer = pointer;
        }

        public IntPtr EquipmentPointer;

        public Prop Prop => new Prop(EquipmentPointer);
    }
}
