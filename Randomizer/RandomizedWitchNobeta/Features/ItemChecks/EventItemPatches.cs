using HarmonyLib;
using RandomizedWitchNobeta.Archipelago;
using RandomizedWitchNobeta.Utils;
using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RandomizedWitchNobeta.Features.ItemChecks;

public static class EventItemPatches
{
    [HarmonyPatch(typeof(CatEvent), nameof(CatEvent.OpenEvent))]
    [HarmonyPrefix]
    private static void CatEventPrefix (CatEvent __instance)
    {
        if (Singletons.RuntimeVariables is not { } runtimeVariables)
        {
            return;
        }

        // Generate a random item
        if (__instance.name is "04_CatAbsorbSkill" or "04_SkillBookAgain" && !runtimeVariables.CatLootObtained)
        {
            var spawnPosition = __instance.name switch
            {
                "04_CatAbsorbSkill" => new Vector3(53.40f, 0.35f, -6.47f),
                "04_SkillBookAgain" => new Vector3(63.30f, 21.69f, -44.72f),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (runtimeVariables.Settings.Archipelago)
            {
                ArchipelagoClient.ServerData.CheckedLocations.Add(ArchipelagoData.GetLocationIdByName("Underground - Cat absorption hint & gift"));
            }
            else if (runtimeVariables.CatOverride != ItemSystem.ItemType.Null)
            {
                Singletons.ItemSystem.itemPoolMap[runtimeVariables.CatOverride]
                    .NewUse(spawnPosition, Quaternion.identity, false);
            }
            // Give souls
            else
            {
                Game.CreateSoul(SoulSystem.SoulType.Money, spawnPosition, Singletons.RuntimeVariables.Settings.ChestSoulCount);
            }

            runtimeVariables.CatLootObtained = true;
        }
    }

    [HarmonyPatch(typeof(CatEvent), nameof(CatEvent.OpenEvent))]
    [HarmonyPostfix]
    private static void CatEventPostfix (CatEvent __instance)
    {
        // Delete game generated absorb book (53.4, 0.4, -6.5) || (63.3, 21.7, -44.7)
        if (__instance.name is "04_CatAbsorbSkill" or "04_SkillBookAgain")
        {
            var spawnPosition = __instance.name switch
            {
                "04_CatAbsorbSkill" => new Vector3(53.4071083f, 0.350891441f, -6.4791069f),
                "04_SkillBookAgain" => new Vector3(63.3090019f, 21.6949997f, -44.7179985f),
                _ => throw new ArgumentOutOfRangeException()
            };

            var items = UnityUtils.FindComponentsByTypeForced<Item>();

            foreach (var item in items)
            {
                if (item.transform.position == spawnPosition)
                {
                    Object.Destroy(item.gameObject);
                }
            }
        }
    }

    [HarmonyPatch(typeof(SceneEvent), nameof(SceneEvent.InitData))]
    [HarmonyPostfix]
    private static void LoadScriptInitPostfix (SceneEvent __instance, SceneEventManager SEM)
    {
        Plugin.Log.LogDebug($"SceneEvent.InitData: {__instance.name} loaded.");

        if (__instance is LoadScript loadScript)
        {
            foreach (var ev in loadScript.Event)
            {
                Plugin.Log.LogDebug($"LoadScript.InitData: {loadScript.name} contains event {ev.name}.");
            }

            if (Singletons.RuntimeVariables is not { } runtimeVariables)
            {
                Plugin.Log.LogError("Unable to load runtime variables.");
                return;
            }

            // Disable thunder from Vanessa
            if (__instance.name is "LoadScriptRoomBossEnd")
            {
                // move event to new collection so it can be executed when item is received
                ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Spirit Realm - Thunder spell from Vanessa V2"), [loadScript.Event[0]]);
                var newEvents = loadScript.Event.SkipLast(1).ToArray();
                loadScript.Event = newEvents;
            }
            // Shrine - first magic barrier
            else if (__instance.name is "Room03_LoadScript" && Singletons.SceneManager.stageId == 2)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
                {
                    // check if item was received already
                    if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Shrine First Magic Barrier")))
                    {
                        // true -> open barrier
                        loadScript.Event[0].OpenEvent();
                        Game.GameSave.flags.stage01Room03 = true;
                    }
                    else
                    {
                        // false -> move event to new collection so it can be executed when item is received
                        ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Shrine First Magic Barrier"), [loadScript.Event[0]]);
                        var newEvents = loadScript.Event.Skip(1).ToArray();
                        loadScript.Event = newEvents;
                    }
                }
                else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // open barrier
                    loadScript.Event[0].OpenEvent();
                    Game.GameSave.flags.stage01Room03 = true;
                }
            }
            // Shrine - second magic barrier trap wall spawn
            else if (__instance.name is "LoadScript_Room04_01" && Singletons.SceneManager.stageId == 2)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // keep barrier open
                    var newEvents = loadScript.Event.Skip(1).ToArray();
                    loadScript.Event = newEvents;
                }
            }
            // Shrine - second magic barrier trap wall destroyed
            else if (__instance.name is "LoadScript_Room04_02" && Singletons.SceneManager.stageId == 2)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
                {
                    // check if item was received already
                    if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Shrine Second Magic Barrier")))
                    {
                        // true -> open barrier
                        loadScript.Event[0].OpenEvent();
                        Game.GameSave.flags.stage01Room04 = true;
                    }
                    else
                    {
                        // false -> move event to new collection so it can be executed when item is received
                        ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Shrine Second Magic Barrier"), [loadScript.Event[0]]);
                        var newEvents = loadScript.Event.Skip(1).ToArray();
                        loadScript.Event = newEvents;
                    }
                }
                else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // open barrier
                    loadScript.Event[0].OpenEvent();
                    Game.GameSave.flags.stage01Room04 = true;
                }
            }
            // Shrine - cat magic barrier trap wall spawn
            else if (__instance.name is "Room05_LoadScript" && Singletons.SceneManager.stageId == 2)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // keep barrier open
                    var newEvents = loadScript.Event.SkipWhile(x => x.name == "04MagicWall01" || x.name == "05MagicWall02").ToArray();
                    loadScript.Event = newEvents;
                }
            }
            // Shrine - cat magic barrier trap wall destroyed
            else if (__instance.name is "Room05_LoadScript02" && Singletons.SceneManager.stageId == 2)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
                {
                    // check if item was received already
                    if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Shrine Meet Cat Magic Barrier")))
                    {
                        // true -> open barrier
                        loadScript.Event[4].OpenEvent();
                        loadScript.Event[5].OpenEvent();
                        Game.GameSave.flags.stage01MeetCat = true;
                    }
                    else
                    {
                        // false -> move event to new collection so it can be executed when item is received
                        ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Shrine Meet Cat Magic Barrier"), [loadScript.Event[0], loadScript.Event[1]]);
                        var newEvents = loadScript.Event.Skip(2).ToArray();
                        loadScript.Event = newEvents;
                    }
                }
                else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // open barrier
                    loadScript.Event[0].OpenEvent();
                    loadScript.Event[1].OpenEvent();
                    Game.GameSave.flags.stage01MeetCat = true;
                }
            }
            // Underground Magic Barrier At Maid Enemy
            else if (__instance.name is "Room06_LoadScript" && Singletons.SceneManager.stageId == 3)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
                {
                    // check if item was received already
                    if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Underground Magic Barrier At Maid Enemy")))
                    {
                        // true -> open barrier
                        loadScript.Event[0].OpenEvent();
                        loadScript.Event[1].OpenEvent();
                        Game.GameSave.flags.stage02Room06 = true;
                    }
                    else
                    {
                        // false -> move event to new collection so it can be executed when item is received
                        ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Underground Magic Barrier At Maid Enemy"), [loadScript.Event[0], loadScript.Event[1]]);
                        var newEvents = loadScript.Event.Skip(2).ToArray();
                        loadScript.Event = newEvents;
                    }
                }
                else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // open barrier
                    loadScript.Event[0].OpenEvent();
                    loadScript.Event[1].OpenEvent();
                    Game.GameSave.flags.stage02Room06 = true;
                }
            }
            // Shrine - first magic barrier
            else if (__instance.name is "Room08_LoadScript" && Singletons.SceneManager.stageId == 3)
            {
                if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
                {
                    // check if item was received already
                    if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Underground Fire Barrier Magic Barrier")))
                    {
                        // true -> open barrier
                        loadScript.Event[0].OpenEvent();
                        loadScript.Event[3].OpenEvent();
                        Game.GameSave.flags.stage02Room08 = true;
                    }
                    else
                    {
                        // false -> move event to new collection so it can be executed when item is received
                        ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Underground Fire Barrier Magic Barrier"), [loadScript.Event[0], loadScript.Event[3]]);
                        var newEvents = loadScript.Event.Skip(1).SkipLast(1).ToArray();
                        loadScript.Event = newEvents;
                    }
                }
                else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
                {
                    // open barrier
                    loadScript.Event[0].OpenEvent();
                    loadScript.Event[3].OpenEvent();
                    Game.GameSave.flags.stage02Room08 = true;
                }
            }
        }
    }

    [HarmonyPatch(typeof(LoadScript), nameof(LoadScript.OpenEvent))]
    [HarmonyPrefix]
    private static void LoadScriptPrefix (LoadScript __instance)
    {
        Plugin.Log.LogDebug($"LoadScript.OpenEvent: {__instance.name} was executed.");
        foreach (var ev in __instance.Event)
        {
            Plugin.Log.LogDebug($"LoadScript.OpenEvent: {__instance.name} contains event {ev.name}.");
        }
    }

    [HarmonyPatch(typeof(OpenDoor), nameof(OpenDoor.OpenEvent))]
    [HarmonyPrefix]
    private static void OpenDoorPrefix (OpenDoor __instance, ref bool __runOriginal)
    {
        Plugin.Log.LogDebug($"OpenDoor.OpenEvent: {__instance.name} was executed.");

        if (Singletons.RuntimeVariables is not { } runtimeVariables)
        {
            Plugin.Log.LogError("OpenDoor.OpenEvent: Unable to load runtime variables.");
            return;
        }

        // "Underground Tania Shortcut Gate On Tania Side"
        if (__instance.name == "DoorBars01" && Singletons.SceneManager.stageId == 3)
        {
            if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
            {
                ArchipelagoClient.ServerData.CheckedLocations.Add(ArchipelagoData.GetLocationIdByName("Underground Tania Shortcut Gate On Tania Side"));
                __instance.g_bOpenEvent = false;
                __runOriginal = false;
            }
        }

        // "Underground Tania Shortcut Gate On Tania Side"
        if (__instance.name == "DoorBars02" && Singletons.SceneManager.stageId == 3)
        {
            if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
            {
                ArchipelagoClient.ServerData.CheckedLocations.Add(ArchipelagoData.GetLocationIdByName("Underground Tania Shortcut Gate On Grand Hall Side"));
                __instance.g_bOpenEvent = false;
                __runOriginal = false;
            }
        }
    }

    [HarmonyPatch(typeof(OpenScriptEvent), nameof(OpenScriptEvent.OpenEvent))]
    [HarmonyPostfix]
    private static void OpenEventPostfix (OpenScriptEvent __instance)
    {
        if (Singletons.RuntimeVariables is not { } runtimeVariables)
        {
            Plugin.Log.LogError("Unable to load runtime variables.");
            return;
        }

        // Shrine - Secret passage magic switch
        if (__instance.ScriptEventType is SaveSystem.ScriptType.L01Room06To07)
        {
            if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.Randomized)
            {
                // check if item was received already
                if (ArchipelagoClient.ServerData.ReceivedItems.Contains(ArchipelagoData.GetItemIdByName("Shrine First Magic Barrier")))
                {
                    // true -> open barrier
                    Game.GameSave.flags.stage01Room06To07 = true;
                }
                else
                {
                    // false -> move event to new collection so it can be executed when item is received
                    ArchipelagoClient.ServerData.StoredEvents.Add(ArchipelagoData.GetItemIdByName("Shrine - Secret passage magic switch"), [__instance]);
                    Game.GameSave.flags.stage01Room06To07 = false;
                }
            }
            else if (runtimeVariables.Settings.MagicPuzzleGateBehaviour == Shared.SeedSettings.MagicPuzzleGateBehaviourType.AlwaysOpen)
            {
                // open barrier
                Game.GameSave.flags.stage01Room06To07 = true;
            }
        }

        Plugin.Log.LogDebug($"OpenScriptEvent.OpenEvent: {__instance.name} with ScriptEventType {__instance.ScriptEventType.ToString()} executed.");
        Plugin.Log.LogDebug($"OpenScriptEvent.OpenEvent: {__instance.name} containes flagdata:");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Cleared: {__instance.Flags.stage01Cleared}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01MeetCat: {__instance.Flags.stage01MeetCat}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01OpenDoor01: {__instance.Flags.stage01OpenDoor01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01OpenDoor02: {__instance.Flags.stage01OpenDoor02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01OpenDoor03: {__instance.Flags.stage01OpenDoor03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room01: {__instance.Flags.stage01Room01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room03: {__instance.Flags.stage01Room03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room04: {__instance.Flags.stage01Room04}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room06To07: {__instance.Flags.stage01Room06To07}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room07Barrier: {__instance.Flags.stage01Room07Barrier}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room07Wall: {__instance.Flags.stage01Room07Wall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room08Door: {__instance.Flags.stage01Room08Door}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room08Wall01: {__instance.Flags.stage01Room08Wall01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room08Wall02: {__instance.Flags.stage01Room08Wall02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room09Barrier: {__instance.Flags.stage01Room09Barrier}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage01Room10: {__instance.Flags.stage01Room10}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02CatBook: {__instance.Flags.stage02CatBook}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Clear: {__instance.Flags.stage02Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02L03BackDoor: {__instance.Flags.stage02L03BackDoor}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02OpenDoor: {__instance.Flags.stage02OpenDoor}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room01: {__instance.Flags.stage02Room01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room02To03Light: {__instance.Flags.stage02Room02To03Light}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room03: {__instance.Flags.stage02Room03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room04Light: {__instance.Flags.stage02Room04Light}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room04Tto05Light01: {__instance.Flags.stage02Room04Tto05Light01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room04Tto05Light02: {__instance.Flags.stage02Room04Tto05Light02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room05Light01: {__instance.Flags.stage02Room05Light01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room05Light02: {__instance.Flags.stage02Room05Light02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room05Tto06Light01: {__instance.Flags.stage02Room05Tto06Light01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room05Tto06Light02: {__instance.Flags.stage02Room05Tto06Light02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room06: {__instance.Flags.stage02Room06}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room08: {__instance.Flags.stage02Room08}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage02Room09: {__instance.Flags.stage02Room09}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Boss01Clear: {__instance.Flags.stage03Boss01Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Clear: {__instance.Flags.stage03Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room01: {__instance.Flags.stage03Room01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room01DoorL: {__instance.Flags.stage03Room01DoorL}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room01DoorR: {__instance.Flags.stage03Room01DoorR}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room02: {__instance.Flags.stage03Room02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room02To04W01: {__instance.Flags.stage03Room02To04W01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room02To04W02: {__instance.Flags.stage03Room02To04W02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room04Event01: {__instance.Flags.stage03Room04Event01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room04Event02: {__instance.Flags.stage03Room04Event02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room04Item: {__instance.Flags.stage03Room04Item}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room04LocationFlag: {__instance.Flags.stage03Room04LocationFlag}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room05: {__instance.Flags.stage03Room05}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room06: {__instance.Flags.stage03Room06}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room08LocationFlag: {__instance.Flags.stage03Room08LocationFlag}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Room08ToBack: {__instance.Flags.stage03Room08ToBack}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage03Stage04BackDoor: {__instance.Flags.stage03Stage04BackDoor}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room01End: {__instance.Flags.stage04Room01End}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room01HatGet: {__instance.Flags.stage04Room01HatGet}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room01HatLost: {__instance.Flags.stage04Room01HatLost}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room01To02CrystalBall: {__instance.Flags.stage04Room01To02CrystalBall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room01To04CrystalBall: {__instance.Flags.stage04Room01To04CrystalBall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room02DoorSwitch: {__instance.Flags.stage04Room02DoorSwitch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room02Switch: {__instance.Flags.stage04Room02Switch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room03CrystalBall: {__instance.Flags.stage04Room03CrystalBall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room04CrystalBall: {__instance.Flags.stage04Room04CrystalBall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room05CrystalBall: {__instance.Flags.stage04Room05CrystalBall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room05DamageWall: {__instance.Flags.stage04Room05DamageWall}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room06: {__instance.Flags.stage04Room06}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room07MoveFloor01: {__instance.Flags.stage04Room07MoveFloor01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room07MoveFloor02: {__instance.Flags.stage04Room07MoveFloor02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room07MoveFloor03: {__instance.Flags.stage04Room07MoveFloor03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room08: {__instance.Flags.stage04Room08}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04Room10Knight: {__instance.Flags.stage04Room10Knight}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage04RoomBossClear: {__instance.Flags.stage04RoomBossClear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room01: {__instance.Flags.stage05Room01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room02DoorSwitch: {__instance.Flags.stage05Room02DoorSwitch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room03To04DoorSwitch: {__instance.Flags.stage05Room03To04DoorSwitch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room04DoorHide: {__instance.Flags.stage05Room04DoorHide}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room04DoorSwitch: {__instance.Flags.stage05Room04DoorSwitch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room04_01: {__instance.Flags.stage05Room04_01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room04_02: {__instance.Flags.stage05Room04_02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room05: {__instance.Flags.stage05Room05}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room06: {__instance.Flags.stage05Room06}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room06DoorHide: {__instance.Flags.stage05Room06DoorHide}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room07_01: {__instance.Flags.stage05Room07_01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room07_02: {__instance.Flags.stage05Room07_02}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room07_03: {__instance.Flags.stage05Room07_03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05Room08DoorHide: {__instance.Flags.stage05Room08DoorHide}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage05RoomBoss: {__instance.Flags.stage05RoomBoss}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act02Alarm: {__instance.Flags.stage06Act02Alarm}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act02Clear: {__instance.Flags.stage06Act02Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act02DoorPlayer: {__instance.Flags.stage06Act02DoorPlayer}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act03Clear: {__instance.Flags.stage06Act03Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act03DamageDoorEnemy: {__instance.Flags.stage06Act03DamageDoorEnemy}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act03DamageDoorPlayer: {__instance.Flags.stage06Act03DamageDoorPlayer}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act04Clear: {__instance.Flags.stage06Act04Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act04Siwtch: {__instance.Flags.stage06Act04Siwtch}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act05Ball: {__instance.Flags.stage06Act05Ball}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Act05Clear: {__instance.Flags.stage06Act05Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06Clear: {__instance.Flags.stage06Clear}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06RoomBoss01: {__instance.Flags.stage06RoomBoss01}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06RoomCentralAct03: {__instance.Flags.stage06RoomCentralAct03}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06RoomCentralAct04: {__instance.Flags.stage06RoomCentralAct04}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06RoomCentralAct05: {__instance.Flags.stage06RoomCentralAct05}");
        Plugin.Log.LogDebug($"Flag __instance.Flags.stage06RoomStart: {__instance.Flags.stage06RoomStart}");
    }
}