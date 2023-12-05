using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using RandomizedWitchNobeta.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomizedWitchNobeta.Archipelago.Net
{
    public static class ArchipelagoConnector
    {
        public static ArchipelagoSession Session { get; set; }

        public static SeedSettings ConnectAP(string server, string slotName, string password)
        {
            SeedSettings settings = null;
            StreamWriter sw = new(Path.Combine("debug.log"), false);

            Session = ArchipelagoSessionFactory.CreateSession(server);
            var result = Session.TryConnectAndLogin("Little Witch Nobeta", slotName, ItemsHandlingFlags.AllItems);
            if(result.Successful)
            {
                sw.WriteLine("connected");
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
                    sw.WriteLine(option_name + "=" + slotData[option_name]);
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
            else
            {
                sw.WriteLine("connection error");
            }
            sw.Close();

            return settings;
        }
    }
}
