using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public abstract class ToggleCheat : Cheat
    {
        private bool enabled = false;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value) return;
                enabled = value;
                if (enabled) OnEnable();
                else OnDisable();
            }
        }

        public ToggleCheat() { }
        public ToggleCheat(KBUtil.Key defaultKeybind) : base(defaultKeybind) { }

        public virtual void Toggle() => Enabled = !Enabled;
        public virtual void OnGUI() { }
        public virtual void Update() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void OnApplicationQuit() { }
    }
}