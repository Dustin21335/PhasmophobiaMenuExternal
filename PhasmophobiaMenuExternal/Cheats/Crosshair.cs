using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Utils;
using System.Numerics;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class Crosshair : ToggleCheat
    {
        public ColorSetting CrosshairColor = new ColorSetting("CrosshairColor", new Vector4(0f, 1f, 0f, 1f));
        public IntValueSetting CrosshairSize = new IntValueSetting("CrosshairSize", 10);
        public IntValueSetting CrosshairThickness = new IntValueSetting("CrosshairThickness", 1);

        public override void OnGUI()
        {
            float X = Program.ScreenSize.X * 0.5f;
            float Y = Program.ScreenSize.Y * 0.5f;
            int size = CrosshairSize.Value;
            int thickness = CrosshairThickness.Value;
            VisualUtil.DrawLine(new Vector2(X - size, Y), new Vector2(X + size, Y), CrosshairColor.Value, thickness);
            VisualUtil.DrawLine(new Vector2(X, Y - size), new Vector2(X, Y + size), CrosshairColor.Value, thickness);
        }
    }
}
