using HarmonyLib;
using Il2CppSystem.Linq.Expressions.Interpreter;
using RandomizedWitchNobeta.Generation.Models;
using RandomizedWitchNobeta.Utils;

namespace RandomizedWitchNobeta.Patches.Shuffle;

public static class ChestContentShufflePatches
{
    [HarmonyPatch(typeof(TreasureBox), nameof(TreasureBox.Init))]
    [HarmonyPostfix]
    private static void InitPostfix(ref TreasureBox __instance)
    {
        if(Singletons.RuntimeVariables is { } runtimeVariables && runtimeVariables.Settings.Archipelago == true)
        {
            __instance.ItemType = ItemSystem.ItemType.Null;
        }
        else if (Singletons.RuntimeVariables is { } runtimeVariables2 && runtimeVariables2.ChestOverrides.TryGetValue(new ChestOverride(__instance.name, Game.sceneManager.stageId), out var itemOverride))
        {
            __instance.ItemType = itemOverride;
        }
        else
        {
            Plugin.Log.LogWarning($"Found unlisted chest shuffle: {__instance.name}");
        }
    }
}
