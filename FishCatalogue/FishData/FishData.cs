﻿using AchtuurCore.Framework.Borders;
using FishCatalogue.Parsing;
using FishCatalogue.Queries;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCatalogue.Data;
internal struct FishData
{
    public string QualifiedID { get; private set; }
    public Item FishItem { get; private set; }
    public string Name => FishItem.DisplayName;

    /// <summary>
    /// All locations where this fish can be caught
    /// </summary>
    public List<string> Locations;

    private SpawnConditions spawnConditions;

    /// <summary>
    /// Returns true if <c>fish_data</c> can be used to construct a <c>FishData</c> object
    /// 
    /// If this function returns false, then constructing a <c>FishData</c> object will throw an exception
    /// 
    /// Doing this instead of try_catch for performance reasons
    /// </summary>
    /// <param name="fish_data"></param>
    /// <returns></returns>
    public static bool IsValidFishData(SpawnFishData fish_data)
    {
        // no item id -> cannot be a fish
        if (string.IsNullOrEmpty(fish_data.ItemId))
            return false;

        // item is not a fish -> not a fish (duh..)
        Item fish_item = ItemRegistry.Create(fish_data.ItemId);
        if (fish_item is null)
            return false;

        return FishCatalogue.AllFishSpawnConditions.ContainsKey(fish_item.Name) 
            || fish_item.Category == StardewValley.Object.FishCategory;

    }

    /// <summary>
    /// Creates a new fishdata using the given <c>fish_data</c> and <c>spawn_fish_data</c>
    /// 
    /// This data is incomplete and should be completed by calling <c>AddLocationData</c> for each location
    /// </summary>
    /// <param name="fish_data">Data for a fish obtained from <c>Data/Fish</c></param>
    /// <param name="spawn_fish_data">Data for a fish obtained from <c>LocationData.Fish</c></param>
    /// <param name="location">Location id of the used <c>LocationData</c></param>
    public FishData(string location_name, SpawnFishData fish_data)
    {
        Locations = new();
        Locations.Add(location_name);
        QualifiedID = fish_data.ItemId;
        FishItem = ItemRegistry.Create(fish_data.ItemId);
    }

    public void AddLocation(string loc_name)
    {
        Locations.Add(loc_name);
    }

    public void AddSpawnConditions(string fish_data)
    {
        spawnConditions = SpawnCondtionsFactory.Create(fish_data);
    }

    public void AddSpawnConditions(SpawnConditions spawnConditions)
    {
        if (spawnConditions is null)
            throw new ArgumentNullException($"spawnConditions are null: {Name}");
        this.spawnConditions = spawnConditions;
    }

    public bool IsCaughtByPlayer()
    {
        if (!Context.IsWorldReady)
            return false;
        return Game1.player.fishCaught.ContainsKey(QualifiedID);
    }

    public bool CanBeCaughtHere()
    {
        if (!Locations.Contains(Game1.currentLocation.Name))
            return false;

        return spawnConditions is null || spawnConditions.CanSpawn();
    }

    public bool CanBeCaughtThisSeason()
    {
        return spawnConditions.CanSpawnThisSeason();
    }

    public IEnumerable<ItemLabel> GenerateSpawnConditionLabel()
    {
        return spawnConditions.ConditionsLabel().Prepend(this.Label());
    }

    public IEnumerable<ItemLabel> GenerateUnfulfilledConditionLabel()
    {
        return spawnConditions.UnfulfilledConditionsLabel().Prepend(this.Label());
    }

    private ItemLabel Label()
    {
        return new ItemLabel(FishItem);
    }
}
