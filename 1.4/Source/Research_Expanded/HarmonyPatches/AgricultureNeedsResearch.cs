using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Research_Expanded.HarmonyPatches;

public class AgricultureNeedsResearch
{
    [HarmonyPatch(typeof(GameRules), nameof(GameRules.DesignatorAllowed))]
    public static class DisableGrowing
    {
        public static Lazy<ResearchProjectDef> GrowingProject = new(() => ResearchProjectDef.Named("CAT_WildCultivation"));

        [HarmonyPostfix]
        public static bool DesignatorAllowed(bool result, Designator d)
        {
            return result && (d is not Designator_ZoneAdd_Growing || (GrowingProject.Value?.IsFinished ?? true));
        }
    }
}
