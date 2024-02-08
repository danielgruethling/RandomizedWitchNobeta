using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using RandomizedWitchNobeta.Generation;
using RandomizedWitchNobeta.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomizedWitchNobeta.Archipelago
{
    public static class ArchipelagoConnector
    {
        public static ArchipelagoSession Session { get; set; }

        private static List<NetworkItem> PendingItemsHelper { get; } = new();

        public static SeedSettings ConnectAP(string server, string slotName, string password)
        {
            SeedSettings settings = null;

            if(Session != null)
            {
                PendingItemsHelper.Clear();
                Session.Items.ItemReceived -= Items_ItemReceived;
                Session?.Socket.DisconnectAsync().Wait();
            }

            Session = ArchipelagoSessionFactory.CreateSession(server);
            Session.Items.ItemReceived += Items_ItemReceived;
            var result = Session.TryConnectAndLogin("Little Witch Nobeta", slotName, ItemsHandlingFlags.AllItems, new("0.4.4"), null, null, password, true);
            if(result.Successful)
            {
                Singletons.APItemReceiveCount = 0;

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

            return settings;
        }

        private static void Items_ItemReceived(ReceivedItemsHelper helper)
        {
            // Handle already received items on reconnection
            /*StreamReader sr = new(Path.Combine("archipelago.cfg"));
            string s = sr.ReadLine();
            sr.Close();
            Plugin.Log.LogMessage($"Read {s}.");
            string[] sSplit = s.Trim().Split('=');
            int receivedCount = Convert.ToInt32(sSplit[1]);
            if (Singletons.APItemReceiveCount < receivedCount)
            {
                // Skip items already received
                helper.DequeueItem();
            }
            else
            {
                // Handle newly received items
                while (helper.Any())
                {
                    PendingItemsHelper.Add(helper.DequeueItem());
                }

                StreamWriter sw = new(Path.Combine("archipelago.cfg"));
                sw.WriteLine($"item_receive_count={Session.Items.AllItemsReceived.Count}");
                sw.Close();
            }*/

            // Handle newly received items
            while (helper.Any())
            {
                var item = helper.DequeueItem();
                Plugin.Log.LogMessage($"Received {item.Item} from {item.Location}");
                PendingItemsHelper.Add(item);
            }

            Singletons.APItemReceiveCount++;

            // Check if not in menu
            if (Singletons.SceneManager.stageId > 1)
            {
                HandleItems();
            }
        }

        public static void HandleItems()
        {
            // Handle newly received items
            while (PendingItemsHelper.Any())
            {
                NetworkItem item = PendingItemsHelper.First();
                string itemName = Session.Items.GetItemName(item.Item);

                Plugin.Log.LogMessage($"Received item count: {Session.Items.AllItemsReceived.Count}. PendingItemsHelperCount {PendingItemsHelper.Count}");

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
                        Singletons.Dispatcher.Enqueue(() =>
                        {
                            Game.CreateSoul(SoulSystem.SoulType.Money, Singletons.WizardGirl.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);
                        });
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

                PendingItemsHelper.RemoveAt(0);

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

                // For trial keys replace first slot that is not a Trial Key and create souls for lost item
                if(itemType == ItemSystem.ItemType.SPMaxAdd)
                {
                    for (int i = 0; i < items.g_iItemSize; i++)
                    {
                        if (items.g_HoldItem[i] != ItemSystem.ItemType.SPMaxAdd)
                        {
                            items.g_HoldItem[i] = itemType;
                            Singletons.StageUi.itemBar.UpdateItemSprite(items.g_HoldItem);
                            Game.CreateSoul(SoulSystem.SoulType.Money, Singletons.WizardGirl.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);

                            return;
                        }
                    }
                }

                // Create souls because item does not fit
                Game.CreateSoul(SoulSystem.SoulType.Money, Singletons.WizardGirl.transform.position, Singletons.RuntimeVariables.Settings.ChestSoulCount);
            });
        }
    }
}
