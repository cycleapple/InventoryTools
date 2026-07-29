using AllaganLib.GameSheets.Sheets;
using AllaganLib.Shared.Interfaces;
using CriticalCommonLib.Crafting;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Debuggers;

public class CraftMonitorDebuggerPane : IDebugPane
{
    private readonly ICraftMonitor _craftMonitor;
    private readonly ItemSheet _itemSheet;

    public CraftMonitorDebuggerPane(ICraftMonitor craftMonitor, ItemSheet itemSheet)
    {
        _craftMonitor = craftMonitor;
        _itemSheet = itemSheet;
    }
    public string Name =>  "Craft Monitor";
    public unsafe void Draw()
    {
        var craftMonitorAgent = _craftMonitor.Agent;
        var simpleCraftMonitorAgent = _craftMonitor.SimpleAgent;
        if (craftMonitorAgent != null)
        {
            ImGui.Text(TF($"Craft Monitor Pointer: {(ulong)craftMonitorAgent.Agent:X}"));
            ImGui.TextUnformatted(T("Is Trial Synthesis: ") + craftMonitorAgent.IsTrialSynthesis);
            ImGui.TextUnformatted(T("Progress: ") + craftMonitorAgent.Progress);
            ImGui.TextUnformatted(T("Total Progress Required: ") +
                _craftMonitor.RecipeLevelTable?.ProgressRequired(_craftMonitor
                    .CurrentRecipe) ?? "Unknown");
            ImGui.TextUnformatted(T("Quality: ") + craftMonitorAgent.Quality);
            ImGui.TextUnformatted(T("Status: ") + craftMonitorAgent.Status);
            ImGui.TextUnformatted(T("Step: ") + craftMonitorAgent.Step);
            ImGui.TextUnformatted(T("Durability: ") + craftMonitorAgent.Durability);
            ImGui.TextUnformatted(T("HQ Chance: ") + craftMonitorAgent.HqChance);
            ImGui.TextUnformatted(T("Item: ") +
                                  (_itemSheet.GetRow(craftMonitorAgent.ResultItemId)
                                      ?.NameString ?? "Unknown"));
            ImGui.TextUnformatted(
                T("Current Recipe: ") + _craftMonitor.CurrentRecipe?.RowId ?? "Unknown");
            ImGui.TextUnformatted(
                T("Recipe Difficulty: ") + _craftMonitor.RecipeLevelTable?.Base.Difficulty ??
                "Unknown");
            ImGui.TextUnformatted(
                T("Recipe Difficulty Factor: ") +
                _craftMonitor.CurrentRecipe?.Base.DifficultyFactor ??
                "Unknown");
            ImGui.TextUnformatted(
                T("Recipe Durability: ") + _craftMonitor.RecipeLevelTable?.Base.Durability ??
                "Unknown");
            ImGui.TextUnformatted(T("Suggested Craftsmanship: ") +
                _craftMonitor.RecipeLevelTable?.Base.SuggestedCraftsmanship ?? "Unknown");
            ImGui.TextUnformatted(
                T("Current Craft Type: ") + _craftMonitor.CraftType ?? "Unknown");
        }
        else if (simpleCraftMonitorAgent != null)
        {
            ImGui.Text(TF($"Simple Craft Monitor Pointer: {(ulong)simpleCraftMonitorAgent.Agent:X}"));
            ImGui.TextUnformatted(T("NQ Complete: ") + simpleCraftMonitorAgent.NqCompleted);
            ImGui.TextUnformatted(T("HQ Complete: ") + simpleCraftMonitorAgent.HqCompleted);
            ImGui.TextUnformatted(T("Failed: ") + simpleCraftMonitorAgent.TotalFailed);
            ImGui.TextUnformatted(T("Total Completed: ") + simpleCraftMonitorAgent.TotalCompleted);
            ImGui.TextUnformatted(T("Total: ") + simpleCraftMonitorAgent.Total);
            ImGui.TextUnformatted(T("Item: ") + _itemSheet
                .GetRowOrDefault(simpleCraftMonitorAgent.ResultItemId)?.NameString.ToString() ?? "Unknown");
            ImGui.TextUnformatted(
                T("Current Recipe: ") + _craftMonitor.CurrentRecipe?.RowId ?? "Unknown");
            ImGui.TextUnformatted(
                T("Current Craft Type: ") + _craftMonitor.CraftType ?? "Unknown");
        }
        else
        {
            ImGui.TextUnformatted(T("Not crafting."));
        }
    }
}