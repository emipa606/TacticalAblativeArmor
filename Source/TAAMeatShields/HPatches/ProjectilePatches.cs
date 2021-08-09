// ProjectilePatches.cs created by Iron Wolf for TAAMeatShields on 08/26/2020 6:33 AM
// last updated 08/26/2020  6:33 AM

using HarmonyLib;
using Verse;

namespace TAAMeatShields.HPatches
{
    [HarmonyPatch(typeof(Projectile))]
    internal static class ProjectilePatches
    {
        private const float FORCE_MISS_RADIUS = 2;

        [HarmonyPatch("CanHit")]
        private static void CanHitPostfix(ref bool __result, Projectile __instance, Thing thing)
        {
            if (!__result || thing == __instance.intendedTarget)
            {
                return;
            }

            var adj = thing.Position.AdjacentToDiagonal(__instance.Launcher.Position);
            if (adj)
            {
                __result = false;
            }
        }
    }
}