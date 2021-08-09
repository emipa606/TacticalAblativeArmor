// CoverUtils.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 2:01 PM
// last updated 08/21/2020  2:01 PM

using System;
using JetBrains.Annotations;
using Verse;

namespace TAAMeatShields
{
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

        public static float BaseBlockChance([NotNull] this Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            if (thing.GetFillage() == FillCategory.Full)
            {
                return 0.75f;
            }

            return thing.GetFillPercentage();
        }


        public static FillCategory GetFillage([NotNull] this Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            var fp = thing.GetFillPercentage();
            if (fp < 0.01f)
            {
                return FillCategory.None;
            }

            if (fp > 0.99f)
            {
                return FillCategory.Full;
            }

            return FillCategory.Partial;
        }

        public static float GetFillPercentage([NotNull] this Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing));
            }

            if (!(thing is Pawn pawn))
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

        public static FillCategory GetFillagePawn([NotNull] this Pawn pawn)
        {
            if (pawn == null)
            {
                throw new ArgumentNullException(nameof(pawn));
            }

            var fp = GetFillPercentPawn(pawn);
            if (fp < 0.01f)
            {
                return FillCategory.None;
            }

            if (fp > 0.99f)
            {
                return FillCategory.Full;
            }

            return FillCategory.Partial;
        }

        public static float GetFillPercentPawn([NotNull] this Pawn pawn)
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