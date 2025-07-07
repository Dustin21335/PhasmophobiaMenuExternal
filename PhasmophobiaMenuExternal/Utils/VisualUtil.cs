using ImGuiNET;
using System.Numerics;

namespace PhasmophobiaMenuExternal.Utils
{
    public class VisualUtil
    {
        public static void DrawText(string text, Vector2 position, Vector4 color)
        {
            Program.DrawList.AddText(position, ImGui.ColorConvertFloat4ToU32(color), text);
        }

        public static void DrawRect(Vector2 topLeft, Vector2 bottomRight, Vector4 color, float thickness = 1f)
        {
            Program.DrawList.AddRect(topLeft, bottomRight, ImGui.ColorConvertFloat4ToU32(color), 0f, ImDrawFlags.None, thickness);
        }

        public static void DrawRectFilled(Vector2 topLeft, Vector2 bottomRight, Vector4 color)
        {
            Program.DrawList.AddRectFilled(topLeft, bottomRight, ImGui.ColorConvertFloat4ToU32(color));
        }

        public static void DrawCircle(Vector2 center, float radius, Vector4 color, float thickness = 1f)
        {
            Program.DrawList.AddCircle(center, radius, ImGui.ColorConvertFloat4ToU32(color), 0, thickness);
        }

        public static void DrawLine(Vector2 point1, Vector2 point2, Vector4 color, float thickness = 1f)
        {
            Program.DrawList.AddLine(point1, point2, ImGui.ColorConvertFloat4ToU32(color), thickness);
        }
    }
}
