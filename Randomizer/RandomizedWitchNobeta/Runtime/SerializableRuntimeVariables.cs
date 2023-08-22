﻿using System.Collections.Generic;
using RandomizedWitchNobeta.Generation;
using RandomizedWitchNobeta.Generation.Models;

namespace RandomizedWitchNobeta.Runtime;

public class SerializableRuntimeVariables
{
    public SeedSettings Settings { get; set; }

    public int GlobalMagicLevel { get; set; }

    public bool CatLootObtained { get; set; } = false;

    public Dictionary<string, bool> KilledBosses { get; set; }
    public HashSet<string> OpenedTrials { get; set; }

    // Generated by the randomizer
    public int StartScene { get; set; }
    public List<KeyValuePair<RegionExit, (int sceneNumberOverride, int savePointOverride)>> ExitsOverrides { get; set; }
    public List<KeyValuePair<ChestOverride, ItemSystem.ItemType>> ChestOverrides { get; set; }
    public ItemSystem.ItemType CatOverride { get; set; }
}