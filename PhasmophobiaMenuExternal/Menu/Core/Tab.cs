namespace PhasmophobiaMenuExternal.Menu.Core
{
    public abstract class Tab
    {
        public Tab(string name, bool enabled = true)
        {
            Name = name;
            Enabled = enabled;
        }

        public string Name;

        public bool Enabled;

        public abstract void Render();
    }
}
