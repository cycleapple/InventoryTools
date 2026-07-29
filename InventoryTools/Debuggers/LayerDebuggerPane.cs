using AllaganLib.Shared.Interfaces;
using Dalamud.Bindings.ImGui;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;

namespace InventoryTools.Debuggers;

public class LayerDebuggerPane : IDebugPane
{
    public string Name => "Layer Debugger";
    public unsafe void Draw()
    {
        var activeLayout = LayoutWorld.Instance()->ActiveLayout;
        if (activeLayout != null)
        {
            ImGui.TextUnformatted(TF($"Level ID: {activeLayout->LevelId}"));
            ImGui.TextUnformatted(TF($"ID: {activeLayout->Id}"));
            ImGui.TextUnformatted(TF($"Type: {activeLayout->Type}"));
            ImGui.TextUnformatted(TF($"Resource Strings: {activeLayout->Type}"));
            foreach (var resourcePath in activeLayout->ResourcePaths.Strings)
            {
                if (resourcePath.Value != null)
                {
                    ImGui.TextUnformatted($"{resourcePath.Value->DataString}");
                }
            }
            ImGui.TextUnformatted(TF($"Layers:"));
            foreach (var layer in activeLayout->Layers)
            {
                ImGui.TextUnformatted($"{layer.Item1}");
                var pointer = layer.Item2.Value;
                if (pointer != null)
                {
                    ImGui.TextUnformatted(TF($"Layer ID: ") + pointer->Id);
                    ImGui.TextUnformatted(TF($"Layer Group ID: ") + pointer->LayerGroupId);
                    ImGui.TextUnformatted(TF($"Festival ID: ") + pointer->FestivalId);
                }
            }
        }
    }
}