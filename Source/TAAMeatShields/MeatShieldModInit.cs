// MeatShieldMod.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 7:26 AM
// last updated 08/21/2020  7:26 AM

using System.Collections.Generic;
using RimWorld;
using Verse;

namespace TAAMeatShields
{
    /// <summary>
    ///     static class for initializing the mod
    /// </summary>
    [StaticConstructorOnStartup]
    public static class MeatShieldModInit
    {
        static MeatShieldModInit() // The one true constructor.
        {
            if (Settings.bodyTypeCoverEffectiveness == null || Settings.bodyTypeCoverEffectiveness.Count == 0)
            {
                Settings.bodyTypeCoverEffectiveness = new Dictionary<string, float>();
                foreach (var bd in DefDatabase<BodyTypeDef>.AllDefsListForReading)
                {
                    Settings.bodyTypeCoverEffectiveness.Add(bd.defName,
                        bd.GetModExtension<BodyTypeDefExtension>().fillPercent);
                }
            }

            foreach (var td in DefDatabase<ThingDef>.AllDefsListForReading)
            {
                if (td.defName.Contains("Corpse_") && td.race != null && td.race.thinkTreeMain.defName == "Humanlike")
                {
                    td.fillPercent = 0.2f;
                }
            }
        }

        private static MeatShieldModSettings Settings =>
            LoadedModManager.GetMod<MeatShieldMod>().GetSettings<MeatShieldModSettings>();
    }
}