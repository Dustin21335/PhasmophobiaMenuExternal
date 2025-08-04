namespace PhasmophobiaMenuExternal.GameSDK.Core
{
    public class MemoryObject
    {
        public IntPtr Address { get; set; }

        public MemoryObject(IntPtr address)
        {
            Address = address;
        }
    }
}
