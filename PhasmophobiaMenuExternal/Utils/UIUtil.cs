using ImGuiNET;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Language;
using System.Numerics;

namespace PhasmophobiaMenuExternal.Utils
{
    public class UIUtil
    {
        public static void Checkbox(string label, ref bool value) => ImGui.Checkbox(Localization.Localize(label), ref value);

        public static void Checkbox(string[] label, ref bool value) => ImGui.Checkbox(Localization.Localize(label), ref value);

        public static void Checkbox(string label, Cheat cheat)
        {
            bool enabled = cheat.Enabled;
            if (ImGui.Checkbox(Localization.Localize(label), ref enabled)) cheat.Enabled = enabled;
        }

        public static void Checkbox(string[] label, Cheat cheat)
        {
            bool enabled = cheat.Enabled;
            if (ImGui.Checkbox(Localization.Localize(label), ref enabled)) cheat.Enabled = enabled;
        }

        public static void Slider(string label, ref int value, int min, int max)
        {
            ImGui.SliderInt(Localization.Localize(label), ref value, min, max);
        }

        public static void Slider(string[] label, ref int value, int min, int max)
        {
            ImGui.SliderInt(Localization.Localize(label), ref value, min, max);
        }

        public static void Slider(string label, ref float value, float min, float max)
        {
            ImGui.SliderFloat(Localization.Localize(label), ref value, min, max);
        }

        public static void Slider(string[] label, ref float value, float min, float max)
        {
            ImGui.SliderFloat(Localization.Localize(label), ref value, min, max);
        }

        public static void Slider(string label, Cheat cheat, float min, float max)
        {
            ImGui.SliderFloat(Localization.Localize(label), ref cheat.Value, min, max);
        }

        public static void Slider(string[] label, Cheat cheat, float min, float max)
        {
            ImGui.SliderFloat(Localization.Localize(label), ref cheat.Value, min, max);
        }

        public static void SliderCheckbox(string label, ref bool enabled, ref float value, float min, float max)
        {
            Checkbox(label, ref enabled);
            SameLine();
            Slider(label, ref value, min, max);
        }

        public static void SliderCheckbox(string label, ref bool enabled, ref int value, int min, int max)
        {
            Checkbox(label, ref enabled);
            SameLine();
            Slider(label, ref value, min, max);
        }

        public static void SliderCheckbox(string[] label, ref bool enabled, ref float value, float min, float max)
        {
            Checkbox(label, ref enabled);
            SameLine();
            Slider(label, ref value, min, max);
        }

        public static void SliderCheckbox(string[] label, ref bool enabled, ref int value, int min, int max)
        {
            Checkbox(label, ref enabled);
            SameLine();
            Slider(label, ref value, min, max);
        }

        public static void SliderCheckbox(string label, TaskCheat taskCheat, int min, int max)
        {
            Checkbox($"##{label}", taskCheat);
            SameLine();
            Slider(label, ref taskCheat.Delay, min, max);
        }

        public static void SliderCheckbox(string label, ref TaskCheat taskCheat, ref int value, int min, int max)
        {
            Checkbox($"##{label}", taskCheat);
            SameLine();
            Slider(label, ref taskCheat.Delay, min, max);
        }

        public static void ColorPicker(string label, ref Vector4 color)
        {
            if (ImGui.CollapsingHeader(Localization.Localize(label))) ImGui.ColorPicker4($"##{label}", ref color);
        }

        public static void ColorPicker(string[] label, ref Vector4 color)
        {
            if (ImGui.CollapsingHeader(Localization.Localize(label))) ImGui.ColorPicker4($"##{label}", ref color);
        }

        public static void Button(string label, Action action)
        {
            if (ImGui.Button(Localization.Localize(label))) action.Invoke();
        }

        public static void Button(string[] label, Action action)
        {
            if (ImGui.Button(Localization.Localize(label))) action.Invoke();
        }

        public static void DropDown<T>(string label, ref T value, T[] values)
        {
            string[] list = values.Select(v => v.ToString()).ToArray();
            int index = Array.IndexOf(values, value);
            if (ImGui.Combo(Localization.Localize(label), ref index, list, list.Length)) value = values[index];
        }

        public static void DropDown<T>(string[] label, ref T value, T[] values)
        {
            string[] list = values.Select(v => v.ToString()).ToArray();
            int index = Array.IndexOf(values, value);
            if (ImGui.Combo(Localization.Localize(label), ref index, list, list.Length)) value = values[index];
        }

        public static void Text(string label, params object[] args)
        {
            ImGui.Text(string.Format(Localization.Localize(label), args));
        }

        public static void Text(string[] label, params object[] args)
        {
            ImGui.Text(string.Format(Localization.Localize(label), args));
        }

        public static void Text(string label, Vector4 color, params object[] args)
        {
            ImGui.TextColored(color, string.Format(Localization.Localize(label), args));
        }

        public static void Text(string[] label, Vector4 color, params object[] args)
        {
            ImGui.TextColored(color, string.Format(Localization.Localize(label), args));
        }

        public static void Text(string label, string label2)
        {
            ImGui.Text($"{Localization.Localize(label)}{label2}");
        }

        public static void Text(string[] label, string[] label2)
        {
            ImGui.Text($"{Localization.Localize(label)}{label2}");
        }

        public static void Text(string[] label, string label2)
        {
            ImGui.Text($"{Localization.Localize(label)}{label2}");
        }

        public static void Text(string label, string[] label2)
        {
            ImGui.Text($"{Localization.Localize(label)}{label2}");
        }

        public static void TextColored(string label, string label2, Vector4 color)
        {
            ImGui.TextColored(color, $"{Localization.Localize(label)}{label2}");
        }

        public static void TextColored(string[] label, string[] label2, Vector4 color)
        {
            ImGui.TextColored(color, $"{Localization.Localize(label)}{label2}");
        }

        public static void TextColored(string[] label, string label2, Vector4 color)
        {
            ImGui.TextColored(color, $"{Localization.Localize(label)}{label2}");
        }

        public static void TextColored(string label, string[] label2, Vector4 color)
        {
            ImGui.TextColored(color, $"{Localization.Localize(label)}{label2}");
        }

        public static void Area(string label, Action action)
        {
            ImGui.Begin(Localization.Localize(label));
            action?.Invoke();
            ImGui.End();
        }

        public static void Area(string[] label, Action action)
        {
            ImGui.Begin(Localization.Localize(label));
            action?.Invoke();
            ImGui.End();
        }

        public static void TabBar(string label, Action action)
        {
            if (ImGui.BeginTabBar(Localization.Localize(label)))
            {
                action?.Invoke();
                ImGui.EndTabBar();
            }
        }

        public static void TabBar(string[] label, Action action)
        {
            if (ImGui.BeginTabBar(Localization.Localize(label)))
            {
                action?.Invoke();
                ImGui.EndTabBar();
            }
        }

        public static void TabItem(string label, Action action)
        {
            if (ImGui.BeginTabItem(Localization.Localize(label)))
            {
                action?.Invoke();
                ImGui.EndTabItem();
            }
        }

        public static void TabItem(string[] label, Action action)
        {
            if (ImGui.BeginTabItem(Localization.Localize(label)))
            {
                action?.Invoke();
                ImGui.EndTabItem();
            }
        }

        public static void Group(string label, Action action)
        {
            ImGui.BeginGroup();
            action?.Invoke();
            ImGui.EndGroup();
        }

        public static void Group(string[] label, Action action)
        {
            ImGui.BeginGroup();
            action?.Invoke();
            ImGui.EndGroup();
        }
        
        public static void SetTooltip(string label)
        {
            ImGui.SetTooltip(Localization.Localize(label));
        }

        public static void SameLine() => ImGui.SameLine();

        public static void Spacing(int amount)
        {
            for (int i = 0; i < amount; i++) ImGui.Spacing();
        }

        public static bool IsItemHovered()
        {
            return ImGui.IsItemHovered();
        }

        public static void InputText(string label, ref string input, uint maxLength)
        {
            ImGui.InputText(Localization.Localize(label), ref input, maxLength);
        }

        public static void InputText(string[] label, ref string input, uint maxLength)
        {
            ImGui.InputText(Localization.Localize(label), ref input, maxLength);
        }
    }
}