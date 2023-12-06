﻿using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using RandomizedWitchNobeta.Behaviours;
using RandomizedWitchNobeta.Generation;
using RandomizedWitchNobeta.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomizedWitchNobeta.Archipelago.Net
{
    public static class ArchipelagoConnector
    {
        public static ArchipelagoSession Session { get; set; }

        public static SeedSettings ConnectAP(string server, string slotName, string password)
        {
            SeedSettings settings = null;

            Session = ArchipelagoSessionFactory.CreateSession(server);
            var result = Session.TryConnectAndLogin("Little Witch Nobeta", slotName, ItemsHandlingFlags.AllItems);
            if(result.Successful)
            {
                settings = new()
                {
                    Archipelago = true,
                    ArchipelagoHostname = server.Split(":").First(),
                    ArchipelagoPort = Convert.ToInt32(server.Split(":")[1]),
                    ArchipelagoSlotName = slotName,
                    ArchipelagoPassword = password
                };

                var loginSuccess = (LoginSuccessful)result;
                Dictionary<string, object> slotData = loginSuccess.SlotData;
                foreach(string option_name in slotData.Keys)
                {
                    switch (option_name)
                    {
                        case "goal":
                            settings.BossHunt = false;
                            settings.MagicMaster = false;
                            switch (Convert.ToInt32(slotData[option_name]))
                            {
                                case 1:
                                    settings.MagicMaster = true;
                                    break;
                                case 2:
                                    settings.BossHunt = true;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "difficulty":
                            settings.Difficulty = (GameDifficulty)(long)slotData[option_name];
                                break;

                        case "trial_keys":
                            settings.TrialKeys = Convert.ToBoolean(slotData[option_name]);
                            break;

                        case "trial_key_amount":
                            settings.TrialKeysAmount = Convert.ToInt32(slotData[option_name]);
                            break;

                        case "no_arcane":
                            settings.NoArcane = Convert.ToBoolean(slotData[option_name]);
                            break;

                        case "entrance_randomization":
                            settings.ShuffleExits = Convert.ToBoolean(slotData[option_name]);
                            break;

                        default:
                            break;
                    }
                }
            }

            Session.Items.ItemReceived += Items_ItemReceived;

            return settings;
        }

        private static void Items_ItemReceived(ReceivedItemsHelper helper)
        {
            while(helper.Any())
            {
                NetworkItem item = helper.DequeueItem();
                string itemName = Session.Items.GetItemName(item.Item);

                switch (itemName)
                {
                    case "Arcane":
                        Singletons.GameSave.stats.secretMagicLevel += 1;
                        break;
                    case "Ice":
                        Singletons.GameSave.stats.iceMagicLevel += 1;
                        break;
                    case "Fire":
                        Singletons.GameSave.stats.fireMagicLevel += 1;
                        break;
                    case "Thunder":
                        Singletons.GameSave.stats.thunderMagicLevel += 1;
                        break;
                    case "Wind":
                        Singletons.GameSave.stats.windMagicLevel += 1;
                        break;
                    case "Mana Absorption":
                        Singletons.GameSave.stats.manaAbsorbLevel += 1;
                        break;
                    case "Progressive Bag Upgrade":
                        Singletons.Dispatcher.Enqueue(() =>
                        {
                            var items = Singletons.WizardGirl.g_PlayerItem;

                            items.g_iItemSize += 1;
                            Singletons.StageUi.itemBar.UpdateItemSize(items.g_iItemSize);
                            Singletons.StageUi.itemBar.UpdateItemSprite(items.g_HoldItem);
                        });
                        break;
                    case "Specter Armor Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Act01");
                        break;
                    case "Enraged Armor Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Act01_Plus");
                        break;
                    case "Tania Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Level02");
                        break;
                    case "Monica Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Level03_Big");
                        break;
                    case "Vanessa Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Level04");
                        break;
                    case "Queen Vanessa V2 Soul":
                        Singletons.RuntimeVariables.KilledBosses.Add("Boss_Level05");
                        break;
                    case "Souls":
                        Game.CreateSoul(SoulSystem.SoulType.Money, Singletons.WizardGirl.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);
                        break;
                    case "HPCure":
                        GiveItem(ItemSystem.ItemType.HPCure);
                        break;
                    case "HPCureMiddle":
                        GiveItem(ItemSystem.ItemType.HPCureMiddle);
                        break;
                    case "HPCureBig":
                        GiveItem(ItemSystem.ItemType.HPCureBig);
                        break;
                    case "MPCure":
                        GiveItem(ItemSystem.ItemType.MPCure);
                        break;
                    case "MPCureMiddle":
                        GiveItem(ItemSystem.ItemType.MPCureMiddle);
                        break;
                    case "MPCureBig":
                        GiveItem(ItemSystem.ItemType.MPCureBig);
                        break;
                    case "Defense":
                        GiveItem(ItemSystem.ItemType.Defense);
                        break;
                    case "DefenseMiddle":
                        GiveItem(ItemSystem.ItemType.DefenseM);
                        break;
                    case "DefenseBig":
                        GiveItem(ItemSystem.ItemType.DefenseB);
                        break;
                    case "Trial Key":
                        GiveItem(ItemSystem.ItemType.SPMaxAdd);
                        break;
                    default:
                        break;
                }

                Singletons.Dispatcher.Enqueue(() =>
                {
                    Game.AppearEventPrompt($"Got {itemName} from {Session.Players.GetPlayerName(item.Player)}'s world ({Session.Locations.GetLocationNameFromId(item.Location)}).");
                });
            }
        }

        private static void GiveItem(ItemSystem.ItemType itemType)
        {
            Singletons.Dispatcher.Enqueue(() =>
            {
                var wizardGirl = Singletons.WizardGirl;
                var items = wizardGirl.g_PlayerItem;

                // Find first empty slot if there's any
                for (int i = 0; i < items.g_iItemSize; i++)
                {
                    if (items.g_HoldItem[i] == ItemSystem.ItemType.Null)
                    {
                        items.g_HoldItem[i] = itemType;
                        Singletons.StageUi.itemBar.UpdateItemSprite(items.g_HoldItem);

                        return;
                    }
                }

                // Replace first slot that is not a Trial Key
                for(int i = 0; i < items.g_iItemSize; i++)
                {
                    if(items.g_HoldItem[i] != ItemSystem.ItemType.SPMaxAdd)
                    {
                        items.g_HoldItem[i] = itemType;
                        Singletons.StageUi.itemBar.UpdateItemSprite(items.g_HoldItem);

                        return;
                    }
                }

                // Drop item because it does not fit
                //TODO
            });
        }
    }
}
