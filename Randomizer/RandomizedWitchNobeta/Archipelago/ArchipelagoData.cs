using System.Collections.Generic;

namespace RandomizedWitchNobeta.Archipelago
{
    public static class ArchipelagoData
    {
        public static string GameLocationToDescriptiveLocation(string gameLocation)
        {
            string descriptiveLocation = "";
            switch(gameLocation)
            {
                // shrine_start_locations
                case "TreasureBox_Room03":
                    descriptiveLocation = "Shrine - Chest Room03_01";
                    break;
                case "TreasureBox02_Room03":
                    descriptiveLocation = "Shrine - Chest Room03_02";
                    break;
                // shrine_armor_locations
                case "TreasureBox_Room05":
                    descriptiveLocation = "Shrine - Chest Room05";
                    break;
                case "Boss_Act01":
                    descriptiveLocation = "Shrine - Specter Armor";
                    break;
                // shrine_secret_passage_locations
                case "TreasureBox07":
                    descriptiveLocation = "Secret Passage - Chest Room07";
                    break;
                case "TreasureBox07To08":
                    descriptiveLocation = "Secret Passage - Chest Room07To08";
                    break;
                case "TreasureBox08":
                    descriptiveLocation = "Secret Passage - Chest Room08";
                    break;
                case "TreasureBox09":
                    descriptiveLocation = "Secret Passage - Chest Room09";
                    break;
                case "TreasureBox10":
                    descriptiveLocation = "Secret Passage - Chest Room10";
                    break;
                case "Boss_Act01_Plus":
                    descriptiveLocation = "Secret Passage - Enraged Armor";
                    break;
                // underground_start_locations
                case "TreasureBox_Room01":
                    descriptiveLocation = "Underground - Chest Room01";
                    break;
                // Handled elsewhere because name appears twice
                /*case "TreasureBox_Room03":
                    descriptiveLocation = "Underground - Chest Room03";
                    break;
                */
                case "TreasureBox_Room04":
                    descriptiveLocation = "Underground - Chest Room04";
                    break;
                case "TreasureBox_Room05_01":
                    descriptiveLocation = "Underground - Chest Room05_01";
                    break;
                case "TreasureBox_Room05_02":
                    descriptiveLocation = "Underground - Chest Room05_02";
                    break;
                case "Cat":
                    descriptiveLocation = "Underground - Cat";
                    break;
                //underground_tania_locations
                case "TreasureBox_Room08":
                    descriptiveLocation = "Underground - Chest Room08";
                    break;
                case "Boss_Level02":
                    descriptiveLocation = "Underground - Tania";
                    break;
                // lava_ruins_start_locations
                case "Room02_TreasureBox01":
                    descriptiveLocation = "Lava Ruins - Chest Room02_01";
                    break;
                case "Room02_TreasureBox02":
                    descriptiveLocation = "Lava Ruins - Chest Room02_02";
                    break;
                case "Room03_TreasureBox01":
                    descriptiveLocation = "Lava Ruins - Chest Room03_01";
                    break;
                case "Room03_TreasureBox02":
                    descriptiveLocation = "Lava Ruins - Chest Room03_02";
                    break;
                case "Room02To04_TreasureBox02":
                    descriptiveLocation = "Lava Ruins - Chest Room02To04";
                    break;
                case "Room05To06_TreasureBox":
                    descriptiveLocation = "Lava Ruins - Chest Room05To06";
                    break;
                case "Room06_TreasureBox":
                    descriptiveLocation = "Lava Ruins - Chest Room06";
                    break;
                // lava_ruins_after_fire_barrier_locations
                case "Room07_TreasureBox":
                    descriptiveLocation = "Lava Ruins - Chest Room07";
                    break;
                case "Room08_TreasureBox":
                    descriptiveLocation = "Lava Ruins - Chest Room08";
                    break;
                case "Boss_Level03_Big":
                    descriptiveLocation = "Lava Ruins - Monica";
                    break;
                case "Room01_TreasureBox":
                    descriptiveLocation = "Lava Ruins - Chest Room01";
                    break;
                // dark_tunnel_start_locations
                case "TreasureBox02_Room02_03":
                    descriptiveLocation = "Dark Tunnel - Chest Room02";
                    break;
                case "TreasureBox02_Room03_01":
                    descriptiveLocation = "Dark Tunnel - Chest Room03_01";
                    break;
                case "TreasureBox02_Room03_02":
                    descriptiveLocation = "Dark Tunnel - Chest Room03_02";
                    break;
                case "TreasureBox02_Room04":
                    descriptiveLocation = "Dark Tunnel - Chest Room04";
                    break;
                case "TreasureBox02_Room05":
                    descriptiveLocation = "Dark Tunnel - Chest Room05";
                    break;
                // dark_tunnel_after_thunder_locations
                case "TreasureBox02_Room06To07":
                    descriptiveLocation = "Dark Tunnel - Chest Room06To07";
                    break;
                case "TreasureBox02_Room07":
                    descriptiveLocation = "Dark Tunnel - Chest Room07";
                    break;
                case "TreasureBox02_Room08":
                    descriptiveLocation = "Dark Tunnel - Chest Room08";
                    break;
                case "TreasureBox02_Room09To10":
                    descriptiveLocation = "Dark Tunnel - Chest Room05";
                    break;
                case "Boss_Level04":
                    descriptiveLocation = "Dark Tunnel - Vanessa";
                    break;
                // spirit_realm_start_locations
                case "TreasureBox02_R02":
                    descriptiveLocation = "Spirit Realm - Chest Room02";
                    break;
                case "TreasureBox02_R03":
                    descriptiveLocation = "Spirit Realm - Chest Room03";
                    break;
                case "TreasureBox02_R0401":
                    descriptiveLocation = "Spirit Realm - Chest Room04To01";
                    break;
                // spirit_realm_after_arcane_barrier_locations
                case "TreasureBox02_R0402":
                    descriptiveLocation = "Spirit Realm - Chest Room04To02";
                    break;
                case "TreasureBox02_R06":
                    descriptiveLocation = "Spirit Realm - Chest Room06";
                    break;
                case "TreasureBox02_R07":
                    descriptiveLocation = "Spirit Realm - Chest Room07";
                    break;
                // spirit_realm_after_teleport_locations
                case "TreasureBox02_R08":
                    descriptiveLocation = "Spirit Realm - Chest Room08";
                    break;
                case "Boss_Level05":
                    descriptiveLocation = "Spirit Realm - Queen Vanessa V2";
                    break;
                // abyss_locations
                case "TreasureBox_Act02Room04":
                    descriptiveLocation = "Abyss - Chest Room04";
                    break;
                case "TreasureBox_Act02Room05":
                    descriptiveLocation = "Abyss - Chest Room05";
                    break;
                // abyss_trials_locations
                case "Act04Room05To06_TreasureBox":
                    descriptiveLocation = "Abyss - Chest Underground Trial";
                    break;
                case "Act05_TreasureBox02_Room09To10":
                    descriptiveLocation = "Abyss - Chest Lava Ruins Trial";
                    break;
                case "Act03TreasureBox_Room05_02":
                    descriptiveLocation = "Abyss - Chest Dark Tunnel Trial";
                    break;
                case "Nonota":
                    descriptiveLocation = "Abyss - Nonota";
                    break;
            }

            return descriptiveLocation;
        }

        public static Dictionary<string, ItemSystem.ItemType> DescriptiveItemToGameItemMap = new()
        {
            {"Arcane", ItemSystem.ItemType.MagicNull },
            {"Ice", ItemSystem.ItemType.MagicIce },
            {"Fire", ItemSystem.ItemType.MagicFire },
            {"Thunder", ItemSystem.ItemType.MagicLightning },
            {"Wind", ItemSystem.ItemType.SkyJump },
            {"Mana Absorption", ItemSystem.ItemType.Absorb },
            {"Progressive Bag Upgrade", ItemSystem.ItemType.BagMaxAdd },
            {"Souls", ItemSystem.ItemType.Null }
        };
    }
}
