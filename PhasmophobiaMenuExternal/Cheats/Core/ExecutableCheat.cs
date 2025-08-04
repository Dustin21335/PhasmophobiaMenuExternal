using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public abstract class ExecutableCheat : Cheat
    {
        public ExecutableCheat() { }
        public ExecutableCheat(KBUtil.Key defaultKeybind) : base(defaultKeybind) { }
        public ExecutableCheat(KBUtil.Key defaultKeybind, bool hidden) : base(defaultKeybind, hidden) { }
        public abstract void Execute();
    }
}
