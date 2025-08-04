namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public class IntValueSetting : Setting<int>
    {
        public IntValueSetting(string name, int value) : base("IntValue", name, value) { }
    }
}
