namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public abstract class Cheat
    {
        public static List<Cheat> instances = new List<Cheat>();
        public static T Instance<T>() where T : Cheat => instances.Find(x => x is T) as T;

        public float Value = -1;
        private bool enabled;
        public bool coreFeature;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    if (enabled)
                    {
                        OnEnable();
                        if (coreFeature) OnEnableCore();
                    }
                    else
                    {
                        OnDisable();
                        if (coreFeature) OnDisableCore();
                    }
                }
            }
        }

        public Cheat()
        {
            instances.Add(this);
        }

        public virtual void OnEnable() { }
        public virtual void OnEnableCore() { }
        public virtual void OnDisable() { }
        public virtual void OnDisableCore() { }
        public virtual void OnApplicationQuit() { }
    }
}