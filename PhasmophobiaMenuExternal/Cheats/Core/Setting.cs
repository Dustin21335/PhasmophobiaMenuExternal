namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public class Setting<T>
    {
        public string Type;
        public string Name;
        public T Value;

        public Setting(string type, string name, T value)
        {
            Type = type;
            Name = name;
            Value = value;
        }
    }
}
