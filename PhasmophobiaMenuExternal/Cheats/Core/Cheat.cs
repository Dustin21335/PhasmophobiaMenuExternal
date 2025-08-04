using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public class Cheat 
    {
        public static List<Cheat> instances = new List<Cheat>();
        public static T Instance<T>() where T : Cheat => instances.Find(x => x is T) as T;

        public KBUtil.Key defaultKeybind = KBUtil.Key.None;
        public KBUtil.Key keybind = KBUtil.Key.None;
        public bool HasKeybind => keybind != KBUtil.Key.None;
        public bool WaitingForKeybind = false;
        public bool Hidden = false;

        public Cheat()
        {
            instances.Add(this);
        }

        public Cheat(KBUtil.Key defaultKeybind)
        {
            this.defaultKeybind = defaultKeybind;
            this.keybind = defaultKeybind;
            instances.Add(this);
        }

        public Cheat(KBUtil.Key defaultKeybind, bool hidden)
        {
            this.defaultKeybind = defaultKeybind;
            this.keybind = defaultKeybind;
            this.Hidden = hidden;
            instances.Add(this);
        }
    }
}