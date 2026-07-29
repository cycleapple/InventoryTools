using System.Collections.Generic;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Bindings.ImGui;
using InventoryTools.Logic.Columns.Abstract;
using InventoryTools.Services;
using Microsoft.Extensions.Logging;

namespace InventoryTools.Logic.Columns
{
    public class DebugCraftColumn : TextColumn
    {
        public DebugCraftColumn(ILogger<DebugCraftColumn> logger, ImGuiService imGuiService) : base(logger, imGuiService)
        {
        }
        public override ColumnCategory ColumnCategory => ColumnCategory.Debug;
        public override string? CurrentValue(ColumnConfiguration columnConfiguration, SearchResult searchResult)
        {
            return "";
        }

        public override List<MessageBase>? Draw(FilterConfiguration configuration,
            ColumnConfiguration columnConfiguration,
            SearchResult searchResult, int rowIndex, int columnIndex)
        {
            if (searchResult.CraftItem == null) return null;

            ImGui.TableNextColumn();
            if (!ImGui.TableGetColumnFlags().HasFlag(ImGuiTableColumnFlags.IsEnabled)) return null;
            ImGui.Text(T("Required: ") +  searchResult.CraftItem.QuantityRequired);
            ImGui.Text(T("Needed: ") +  searchResult.CraftItem.QuantityNeeded);
            ImGui.Text(T("Needed Pre Update: ") +  searchResult.CraftItem.QuantityNeededPreUpdate);
            ImGui.Text(T("Available: ") +  searchResult.CraftItem.QuantityAvailable);
            ImGui.Text(T("Ready: ") +  searchResult.CraftItem.QuantityReady);
            ImGui.Text(T("Can Craft: ") +  searchResult.CraftItem.QuantityCanCraft);
            ImGui.Text(T("Will Retrieve: ") + searchResult.CraftItem.QuantityWillRetrieve);
            return null;
        }

        public override string Name { get; set; } = "Debug - Craft";
        public override float Width { get; set; } = 200;
        public override string HelpText { get; set; } = "Shows craft debug information";
        public override bool HasFilter { get; set; } = true;
        public override bool IsDebug { get; set; } = true;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}