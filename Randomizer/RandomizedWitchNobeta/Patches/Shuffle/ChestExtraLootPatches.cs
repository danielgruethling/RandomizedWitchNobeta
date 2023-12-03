using HarmonyLib;
using RandomizedWitchNobeta.Archipelago.Net;
using RandomizedWitchNobeta.Utils;
using System.IO;

namespace RandomizedWitchNobeta.Patches.Shuffle;

public static class ChestExtraLootPatches
{
    [HarmonyPatch(typeof(TreasureBox), nameof(TreasureBox.SetOpen))]
    [HarmonyPostfix]
    private static void OpenPostfix(TreasureBox __instance)
    {
        if (__instance.ItemType == ItemSystem.ItemType.Null)
        {
            if (Singletons.RuntimeVariables is { } runtimeVariables && runtimeVariables.Settings.Archipelago == true)
            {
                StreamWriter sw = new(Path.Combine("debug.log"));
                ArchipelagoConnector.Session.Locations.CompleteLocationChecks(ArchipelagoConnector.Session.Locations.GetLocationIdFromName("Little Witch Nobeta", __instance.name));
                sw.WriteLine(__instance.name);
                sw.Close();
            }
            else
            {
                Game.CreateSoul(SoulSystem.SoulType.Money, __instance.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);
            }
        }
    }
}