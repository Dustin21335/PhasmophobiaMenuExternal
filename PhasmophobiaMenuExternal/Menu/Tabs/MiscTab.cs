using PhasmophobiaMenuExternal.Cheats;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;
using System.Numerics;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class MiscTab : Tab
    {
        public MiscTab() : base("MiscTab") { }

        private string posX = "0";
        private string posY = "0";
        private string posZ = "0";

        public override void Render()
        {
            UIUtil.TabItem("MiscTab.Title", () =>
            {
                UIUtil.Checkbox("MiscTab.Crosshair", Cheat.Instance<Crosshair>());
                UIUtil.Slider("MiscTab.CrosshairSize", ref Cheat.Instance<Crosshair>().CrosshairSize.Value, 1, 50);
                UIUtil.Slider("MiscTab.CrosshairThickness", ref Cheat.Instance<Crosshair>().CrosshairThickness.Value, 1, 25);
                UIUtil.Text("MiscTab.Teleportion");
                UIUtil.InputText("MiscTab.X", ref posX, 5);
                UIUtil.InputText("MiscTab.Y", ref posY, 5);
                UIUtil.InputText("MiscTab.Z", ref posZ, 5);
                UIUtil.Button("MiscTab.SaveCurrentPosition", () =>
                {
                    Vector3 position = GameObjectManager.LocalPlayer?.LocalPlayerPosition ?? Vector3.Zero;
                    posX = position.X.ToString();
                    posY = position.Y.ToString();
                    posZ = position.Z.ToString();
                });
                UIUtil.SameLine();
                UIUtil.Button("MiscTab.Teleport", () =>
                {
                    if (float.TryParse(posX, out float x) && float.TryParse(posY, out float y) && float.TryParse(posZ, out float z) && GameObjectManager.LocalPlayer != null) GameObjectManager.LocalPlayer.LocalPlayerPosition = new Vector3(x, y, z);
                });
            });
        }
    }
}
