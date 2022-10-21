using System.Collections.Generic;
using Verse;

namespace TAAMeatShields;

public class MeatShieldModSettings : ModSettings
{
    public Dictionary<string, float> bodyTypeCoverEffectiveness;
    public float coverEffectiveness = 0.3f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref coverEffectiveness, nameof(coverEffectiveness));
        Scribe_Collections.Look(ref bodyTypeCoverEffectiveness, nameof(bodyTypeCoverEffectiveness));
    }
}