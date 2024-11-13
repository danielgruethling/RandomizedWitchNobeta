using MessagePack;
using System.IO.Hashing;
using System.Text;

namespace RandomizedWitchNobeta.Shared;

[MessagePackObject]
public class SeedSettings
{
    public enum GameDifficulty
    {
        Standard,
        Advanced,
        Hard,
        BossRush
    }

    public enum MagicUpgradeMode
    {
        Vanilla,
        BossKill
    }

    public enum StartLevelSetting
    {
        Random,
        OkunShrine,
        UndergroundCave,
        LavaRuins,
        DarkTunnel,
        SpiritRealm
    }

    public enum ShortcutGateBehaviourType
    {
        Vanilla,
        AlwaysOpen,
        Randomized,
    }

    public enum MagicPuzzleGateBehaviourType
    {
        Vanilla,
        AlwaysOpen,
        Randomized,
    }

    // General

    [Key(0)]
    public int Seed { get; set; } = Random.Shared.Next();

    [Key(1)]
    public GameDifficulty Difficulty { get; set; } = GameDifficulty.Advanced;

    [Key(2)]
    public bool ShuffleExits { get; set; } = true;

    [Key(3)]
    public StartLevelSetting StartLevel { get; set; } = StartLevelSetting.Random;

    [Key(4)]
    public bool GameHints { get; set; } = true;

    // Extra End Conditions

    [Key(5)]
    public bool MagicMaster { get; set; } = false;

    [Key(6)]
    public bool BossHunt { get; set; } = false;

    [Key(7)]
    public bool AllChestOpened { get; set; } = false;

    [Key(8)]
    public bool TrialKeys { get; set; } = false;

    [Key(9)]
    public int TrialKeysAmount { get; set; } = 5;

    // Magic

    [Key(10)]
    public MagicUpgradeMode MagicUpgrade { get; set; } = MagicUpgradeMode.Vanilla;

    [Key(11)]
    public bool NoArcane { get; set; } = false;

    [Key(12)]
    public int BookAmount { get; set; } = 1;

    // Combat
    [Key(13)]
    public bool OneHitKO { get; set; } = false;

    [Key(14)]
    public bool DoubleDamage { get; set; } = false;

    [Key(15)]
    public bool HalfDamage { get; set; } = false;

    // Balance
    [Key(16)]
    public int ChestSoulCount { get; set; } = 250;

    [Key(17)]
    public float StartSoulsModifier { get; set; } = 1f;

    // Weights
    [Key(18)]
    public int ItemWeightSouls { get; set; } = 3;

    [Key(19)]
    public int ItemWeightHP { get; set; } = 1;

    [Key(20)]
    public int ItemWeightMP { get; set; } = 1;

    [Key(21)]
    public int ItemWeightDefense { get; set; } = 1;

    [Key(22)]
    public int ItemWeightHoly { get; set; } = 1;

    [Key(23)]
    public int ItemWeightArcane { get; set; } = 2;

    [Key(24)]
    public bool Archipelago = false;

    [Key(25)]
    public string ArchipelagoHostname = "archipelago.gg";

    [Key(26)]
    public int ArchipelagoPort = 38281;

    [Key(27)]
    public string ArchipelagoSlotName = "Player";

    [Key(28)]
    public string ArchipelagoPassword = string.Empty;

    [Key(29)]
    public ShortcutGateBehaviourType ShortcutGateBehaviour = ShortcutGateBehaviourType.Vanilla;

    [Key(30)]
    public MagicPuzzleGateBehaviourType MagicPuzzleGateBehaviour = MagicPuzzleGateBehaviourType.Vanilla;

    public int Hash(string gameVersionText, string randomizerVersionText)
    {
        // This is not ideal but it gives consistent results
        return BitConverter.ToInt32(
            Crc32.Hash(Encoding.UTF8.GetBytes(SerializeUtils.SerializeIndented(this) + gameVersionText + randomizerVersionText))
        );
    }
}