namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public class ListValueSetting<T> : Setting<List<T>>
    {
        public ListValueSetting(string name, List<T> value) : base("ListValue", name, value) { }
    }
}
