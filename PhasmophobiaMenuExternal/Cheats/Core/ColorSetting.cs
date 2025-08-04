using System.Numerics;

namespace PhasmophobiaMenuExternal.Cheats.Core
{
    public class ColorSetting : Setting<Vector4>
    {
        public ColorSetting(string name, Vector4 color) : base("Colors", name, color) { }
    }
}
