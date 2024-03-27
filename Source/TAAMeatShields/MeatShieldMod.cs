// MeatShieldMod.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 7:26 AM
// last updated 08/21/2020  7:26 AM

using System.Linq;
using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace TAAMeatShields;

public class MeatShieldMod : Mod
{
    public const string MOD_ID = "tachyonite.meatshields";
    private static string currentVersion;

    public MeatShieldMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<MeatShieldModSettings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }


    public MeatShieldModSettings Settings { get; }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);

        foreach (var bd in Settings.bodyTypeCoverEffectiveness.ToArray())
        {
            var loadedDef = DefDatabase<BodyTypeDef>.GetNamedSilentFail(bd.Key);
            var val = bd.Value;
            listingStandard.GapLine();
            listingStandard.Label($"{loadedDef.defName}: {val:P0}");
            val = listingStandard.Slider(val, 0f, 0.99f);
            loadedDef.GetModExtension<BodyTypeDefExtension>().fillPercent = bd.Value;
            Settings.bodyTypeCoverEffectiveness[bd.Key] = val;
        }

        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("MeatShieldModversion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    /// <summary>
    ///     Override SettingsCategory to show up in the list of settings. <br />
    ///     Using .Translate() is optional, but does allow for localisation.
    /// </summary>
    /// <returns> The (translated) mod name. </returns>
    public override string SettingsCategory()
    {
        return "MeatShieldModName".Translate();
    }
}