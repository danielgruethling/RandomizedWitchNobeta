using System.Collections.Generic;

namespace RandomizedWitchNobeta.Utils.Nobeta;

public static class NpcUtils
{
    public const string ArmorBossName = "Boss_Act01";
    public const string TaniaBossName = "Boss_Level02";
    public const string MonicaBossName = "Boss_Level03_Big";
    public const string Vanessa1BossName = "Boss_Level04";
    public const string Vanessa2BossName = "Boss_Level05";
    public const string NonotaBossName = "Boss_Level06";

    public static readonly HashSet<string> ValidBosses =
    [
        ArmorBossName, // Armor
        TaniaBossName, // Tania
        MonicaBossName, // Monica
        Vanessa1BossName, // Vanessa
        Vanessa2BossName, // Vanessa 2
        "Boss_Act01_Plus"
    ];
}