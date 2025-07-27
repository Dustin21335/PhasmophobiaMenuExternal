using PhasmophobiaMenuExternal.Cheats.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PhasmophobiaMenuExternal.Cheats
{
    public class GammaHack : TaskCheat
    {
        public GammaHack()
        {
            Value = 2.0f;
        }

        private float OriginalGamma = -1f;

        public override void OnEnable()
        {
            if (OriginalGamma == -1f) OriginalGamma = GetGamma();
            SetGamma(Value);
        }

        public override void OnDisable()
        {
            SetGamma(OriginalGamma);
            OriginalGamma = -1f;
        }

        public override Task Update()
        {
            if (OriginalGamma != -1f)
            {
                if (!IsProcessFocused(Program.SimpleMemoryReading.Process) && !IsProcessFocused(Process.GetCurrentProcess())) SetGamma(OriginalGamma);
                else if (GetGamma() != Value) SetGamma(Value);
            }
            return Task.CompletedTask;
        }

        public override void OnApplicationQuit()
        {
            if (OriginalGamma == -1f) return;
            SetGamma(OriginalGamma);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private static extern bool SetDeviceGammaRamp(IntPtr hDC, IntPtr lpRamp);

        [DllImport("gdi32.dll")]
        private static extern bool GetDeviceGammaRamp(IntPtr hDC, IntPtr lpRamp);

        public static void SetGamma(float gamma)
        {
            if (gamma < 0.1f || gamma > 4.45f) return;
            IntPtr hDC = GetDC(IntPtr.Zero);
            IntPtr rampPtr = Marshal.AllocHGlobal(3 * 256 * sizeof(ushort));
            for (int i = 0; i < 256; i++)
            {
                int value = (int)(Math.Pow(i / 255.0, 1.0 / gamma) * 65535 + 0.5);
                if (value > 65535) value = 65535;
                ushort v = (ushort)value;
                for (int j = 0; j < 3; j++) Marshal.WriteInt16(rampPtr, (j * 256 + i) * 2, (short)v);
            }
            if (!SetDeviceGammaRamp(hDC, rampPtr)) Console.WriteLine("Failed to set gamma");
            Marshal.FreeHGlobal(rampPtr);
        }

        private static float GetGamma()
        {
            IntPtr hDC = GetDC(IntPtr.Zero);
            IntPtr rampPtr = Marshal.AllocHGlobal(3 * 256 * sizeof(ushort));
            float gamma = 1.0f;
            if (GetDeviceGammaRamp(hDC, rampPtr))
            {
                double total = 0;
                int count = 0;
                for (int i = 16; i < 240; i += 16)
                {
                    double input = i / 255.0;
                    double output = Marshal.ReadInt16(rampPtr, i * 2) / 65535.0;
                    if (input > 0 && output > 0)
                    {
                        total += Math.Log(output) / Math.Log(input);
                        count++;
                    }
                }
                if (count > 0) gamma = (float)(1.0 / (total / count));
            }
            Marshal.FreeHGlobal(rampPtr);
            return gamma;
        }

        private static bool IsProcessFocused(Process process)
        {
            if (process == null || process.HasExited) return false;
            IntPtr foregroundWindow = GetForegroundWindow();
            return foregroundWindow == process.MainWindowHandle && foregroundWindow != IntPtr.Zero;
        }
    }
}