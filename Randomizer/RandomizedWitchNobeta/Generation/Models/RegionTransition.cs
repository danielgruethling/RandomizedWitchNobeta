using RandomizedWitchNobeta.Generation.Models.Requirements;

namespace RandomizedWitchNobeta.Generation.Models;

public class RegionTransition
{
    public Region Destination { get; init; }
    public ITransitionRequirement Requirement { get; init; }
}