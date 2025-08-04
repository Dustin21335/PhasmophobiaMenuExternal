using PhasmophobiaMenuExternal.GameSDK;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;
using System.Numerics;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class PlayerTab : Tab
    {
        public PlayerTab() : base("PlayerTab") { }

        private Player SelectedPlayer;

        public override void Render()
        {
            UIUtil.TabItem("PlayerTab.Title", () =>
            {
                UIUtil.Columns(2, "PlayerTabColumns", true);
                UIUtil.AreaChild("PlayerList", () =>
                {
                    if (GameObjectManager.Players != null && GameObjectManager.Players.Count > 0)
                    {
                        int index = 0;
                        foreach (Player player in GameObjectManager.Players)
                        {
                            if (player == null) continue;
                            Vector4 color = player == SelectedPlayer ? new Vector4(0f, 1f, 0f, 1f) : new Vector4(1f, 1f, 1f, 1f);
                            if (UIUtil.Selectable($"{player.PhotonView?.Owner?.NickName ?? "Unknown"}##{index}", player == SelectedPlayer)) SelectedPlayer = player;
                            index++;
                        }
                    }
                });
                UIUtil.NextColumn();
                UIUtil.AreaChild("Actions", () =>
                {
                    if (SelectedPlayer != null)
                    {
                        PhotonPlayer photonPlayer = SelectedPlayer.PhotonView?.Owner;
                        Vector3 position = SelectedPlayer.PhysicsCharacterController?.Position ?? Vector3.Zero;
                        UIUtil.Text("PlayerTab.Name", $": {photonPlayer?.NickName ?? "Unknown"}");
                        UIUtil.Text("PlayerTab.ActorNumber", $": {photonPlayer?.ActorNumber ?? 0}");
                        UIUtil.Text("PlayerTab.CurrentRoom", $": {SelectedPlayer.CurrentRoom?.Name ?? "Unknown"}");
                        UIUtil.Text("PlayerTab.Stamina", $": {SelectedPlayer.PlayerStamina?.Stamina ?? 0}");
                        UIUtil.Text("PlayerTab.Sanity", $": {SelectedPlayer.PlayerSanity?.Sanity ?? 0}");
                        UIUtil.Text("PlayerTab.Position", $": {position.X.ToString("F1")}, {position.Y.ToString("F1")}, {position.Z.ToString("F1")}");
                        if (!photonPlayer.IsLocal) UIUtil.Button("PlayerTab.TeleportToPlayer", () => GameObjectManager.LocalPlayer.LocalPlayerPosition = position + new Vector3(0f, 0.7f, 0f));
                    }
                    else UIUtil.Text("PlayerTab.NoPlayerSelected");
                });
                UIUtil.Columns(1, "PlayerTabColumnsEnd");
            });
        }
    }
}
