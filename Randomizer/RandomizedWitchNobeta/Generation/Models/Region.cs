namespace RandomizedWitchNobeta.Generation.Models;

public class Region
{
    public string FriendlyName { get; init; }

    public RegionExit[] Exits { get; init; }

    public ItemLocation[] ItemLocations { get; init; }

    public RegionTransition NextRegion { get; init; }

    public bool FinalRegion { get; init; } = false;

    public bool ContainsBoss { get; set; } = false;
}