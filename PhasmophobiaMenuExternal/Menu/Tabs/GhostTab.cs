using PhasmophobiaMenuExternal.GameSDK;
using PhasmophobiaMenuExternal.Menu.Core;
using PhasmophobiaMenuExternal.Utils;

namespace PhasmophobiaMenuExternal.Menu.Tabs
{
    public class GhostTab : Tab
    {
        public GhostTab() : base("GhostTab") { }

        public override void Render()
        {
            UIUtil.TabItem("GhostTab.Title", () =>
            {
                UIUtil.Text("GhostTab.Name", $": {GameObjectManager.LocalPlayer?.JournalController?.GhostName?.Text ?? "Unknown"}");
                UIUtil.Text("GhostTab.Age", $": {GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.Age ?? 0}");
                UIUtil.Text("GhostTab.Type", $": {GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.GhostType ?? GhostTraits.GhostTypes.None}");
                if (GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.GhostType == GhostTraits.GhostTypes.Banshee) UIUtil.Text("GhostTab.BansheeTarget", $": {GameObjectManager.GhostAI?.BansheeTarget?.PhotonView?.Owner?.NickName ?? "Unknown"}");
                else if (GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.GhostType == GhostTraits.GhostTypes.Mimic) UIUtil.Text("GhostTab.MimicType", $": {GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.MimicGhostType ?? GhostTraits.GhostTypes.None}");
                UIUtil.Text("GhostTab.AllPossibleEvidence", $": {(GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.AllPossibleEvidence?.Count > 0 ? string.Join(", ", GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.AllPossibleEvidence) : "Unknown")}");
                UIUtil.Text("GhostTab.AllEvidence", $": {(GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.AllEvidence?.Count > 0 ? string.Join(", ", GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.AllEvidence) : "Unknown")}");
                UIUtil.Text("GhostTab.CurrentRoom", $": {GameObjectManager.GhostAI?.GhostActivity?.LevelController?.GhostCurrentRoom?.Name ?? "Unknown"}");
                UIUtil.Text("GhostTab.FavoriteRoom", $": {GameObjectManager.GhostAI?.GhostInfo?.FavoriteRoom?.Name ?? "Unknown"}");
                UIUtil.Text("GhostTab.State", $": {GameObjectManager.GhostAI?.GhostState ?? GhostAI.GhostStates.Idle}");
                UIUtil.Text("GhostTab.IsMale", $": {GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.IsMale ?? false}");
                UIUtil.Text("GhostTab.IsShy", $": {GameObjectManager.GhostAI?.GhostInfo?.GhostTraits?.IsShy ?? false}");
                UIUtil.Text("GhostTab.IsHunting", $": {GameObjectManager.GhostAI?.IsHunting ?? false}");
            });
        }
    }
}
