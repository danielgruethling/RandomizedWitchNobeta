using Archipelago.MultiClient.Net;
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
                    case "Progressive Bag Upgrade": //todo think about how to increase bag
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
                    default:
                        break;
                }

                UnityMainThreadDispatcher.Instance.Enqueue(() =>
                {
                    Game.AppearEventPrompt($"Got {itemName} from {Session.Players.GetPlayerName(item.Player)}'s world ({Session.Locations.GetLocationNameFromId(item.Location)}).");
                });
            }
        }
    }
}
