// CoverUtils.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 2:01 PM
// last updated 08/21/2020  2:01 PM

using System;
using JetBrains.Annotations;
using Verse;

namespace TAAMeatShields;

public static class CoverUtils
{
    private static float? _fillPercentageSettingMult;

    private static float FillPercentageMult
    {
        get
        {
            if (_fillPercentageSettingMult != null)
            {
                return _fillPercentageSettingMult.Value;
            }

            var ms = LoadedModManager.GetMod<MeatShieldMod>().Settings.coverEffectiveness;
            _fillPercentageSettingMult = ms;

            return _fillPercentageSettingMult.Value;
        }
    }

    extension([NotNull] Thing thing)
    {
        public float BaseBlockChance()
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            return thing.GetFillage() == FillCategory.Full ? 0.75f : thing.GetFillPercentage();
        }

        public FillCategory GetFillage()
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            var fp = thing.GetFillPercentage();
            switch (fp)
            {
                case < 0.01f:
                    return FillCategory.None;
                case > 0.99f:
                    return FillCategory.Full;
                default:
                    return FillCategory.Partial;
            }
        }

        public float GetFillPercentage()
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            if (thing is not Pawn pawn)
            {
                return thing.def.fillPercent;
            }

            var bodyType = pawn.story?.bodyType;
            var fp = bodyType?.GetModExtension<BodyTypeDefExtension>()?.fillPercent;
            var coverOut = pawn.def.fillPercent * thing.def.race.baseBodySize;
            if (coverOut > 0.99f)
            {
                coverOut = 0.99f;
            }

            return fp ?? coverOut;
        }
    }


    extension([NotNull] Pawn pawn)
    {
        public FillCategory GetFillagePawn()
        {
            if (pawn == null)
            {
                throw new ArgumentNullException(nameof(pawn));
            }

            var fp = pawn.getFillPercentPawn();
            switch (fp)
            {
                case < 0.01f:
                    return FillCategory.None;
                case > 0.99f:
                    return FillCategory.Full;
                default:
                    return FillCategory.Partial;
            }
        }

        private float getFillPercentPawn()
        {
            var bodyType = pawn.story?.bodyType;
            var fp = bodyType?.GetModExtension<BodyTypeDefExtension>()?.fillPercent;
            var coverOut = pawn.def.fillPercent * pawn.def.race.baseBodySize;
            if (coverOut > 0.99f)
            {
                coverOut = 0.99f;
            }

            return fp ?? coverOut;
        }
    }
}