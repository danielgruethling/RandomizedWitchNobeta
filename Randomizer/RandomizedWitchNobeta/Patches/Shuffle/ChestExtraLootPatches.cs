using HarmonyLib;
using RandomizedWitchNobeta.Archipelago;
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
                // Special exception because TreasureBox instance name TreasureBox_Room03 exists twice
                if (Game.sceneManager.stageId == 3 && __instance.name == "TreasureBox_Room03")
                {
                    ArchipelagoConnector.Session.Locations.CompleteLocationChecks(ArchipelagoConnector.Session.Locations.GetLocationIdFromName("Little Witch Nobeta", "Underground - Chest Room03"));
                }
                else
                {
                    ArchipelagoConnector.Session.Locations.CompleteLocationChecks(ArchipelagoConnector.Session.Locations.GetLocationIdFromName("Little Witch Nobeta", ArchipelagoData.GameLocationToDescriptiveLocationMap(__instance.name)));
                }
                StreamWriter sw = new(Path.Combine("debug.log"), true);
                sw.WriteLine($"Found location: {__instance.name}");
                sw.Close();
                sw = new(Path.Combine("debug.log"), true);
                sw.WriteLine($"Found location: {__instance.index}");
                sw.Close();
                sw = new(Path.Combine("debug.log"), true);
                sw.WriteLine("Send location name" + ArchipelagoData.GameLocationToDescriptiveLocationMap(__instance.name));
                sw.Close();
                sw = new(Path.Combine("debug.log"), true);
                sw.WriteLine($"Translated to {ArchipelagoConnector.Session.Locations.GetLocationIdFromName("Little Witch Nobeta", ArchipelagoData.GameLocationToDescriptiveLocationMap(__instance.name))} to AP server.");
                sw.Close();
            }
            else
            {
                Game.CreateSoul(SoulSystem.SoulType.Money, __instance.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);
            }
        }
    }
}