// MeatShieldMod.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 7:26 AM
// last updated 08/21/2020  7:26 AM

using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using System.Linq;

namespace TAAMeatShields
{
    public class MeatShieldMod : Mod
    {
        public const string MOD_ID = "tachyonite.meatshields";


        public MeatShieldModSettings Settings { get; }

        public MeatShieldMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<MeatShieldModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label($"{"CoverEffectiveness".Translate()}: {Settings.coverEffectiveness.ToString("P0")}");
            Settings.coverEffectiveness = listingStandard.Slider(Settings.coverEffectiveness, 0f, 0.99f);

            foreach (KeyValuePair<BodyTypeDef,float> bd in Settings.bodyTypeCoverEffectiveness.ToArray())
            {
                var val = bd.Value;
                listingStandard.GapLine();
                listingStandard.Label($"{bd.Key.defName}: {val.ToString("P0")}");
                val = listingStandard.Slider(val, 0f, 0.99f);
                bd.Key.GetModExtension<BodyTypeDefExtension>().fillPercent = bd.Value;
                Settings.bodyTypeCoverEffectiveness[bd.Key] = val;
            }

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings. <br />
        /// Using .Translate() is optional, but does allow for localisation.
        /// </summary>
        /// <returns> The (translated) mod name. </returns>
        public override string SettingsCategory()
        {
            return "MeatShieldModName".Translate();
        }
    }

    public class MeatShieldModSettings : ModSettings
    {
        public float coverEffectiveness = 0.3f;

        public Dictionary<BodyTypeDef,float> bodyTypeCoverEffectiveness;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref coverEffectiveness, nameof(coverEffectiveness));
            Scribe_Collections.Look(ref bodyTypeCoverEffectiveness, nameof(bodyTypeCoverEffectiveness));
        }
    }

}