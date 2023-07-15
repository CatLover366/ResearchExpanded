using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Research_Expanded
{
    [StaticConstructorOnStartup]
    public static class Init
    {
        static Init()
        {
            ResearchProjectTagDef sparkTag = DefDatabase<ResearchProjectTagDef>.GetNamed("CAT_ResearchExpandedSpark");
            var sparks = DefDatabase<ResearchProjectDef>.AllDefs.Where(r => r.HasTag(sparkTag)).ToDictionary(r => r.techLevel);
            foreach (ResearchProjectDef researchProjectDef in DefDatabase<ResearchProjectDef>.AllDefs)
            {
                ResearchProjectDef spark = sparks.TryGetValue(researchProjectDef.HasTag(sparkTag) ? researchProjectDef.techLevel - 1 : researchProjectDef.techLevel);
                if (spark == null) return;

                researchProjectDef.prerequisites ??= new List<ResearchProjectDef>();
                if (researchProjectDef.prerequisites.Count == 0 ||
                    researchProjectDef.prerequisites.All(p => p.techLevel < researchProjectDef.techLevel))
                {
#if DEBUG
    Log.Message($"Adding spark {spark.LabelCap} to research {researchProjectDef.LabelCap} at tech level {researchProjectDef.techLevel}");
#endif
                    researchProjectDef.prerequisites.Add(spark);
                }
            }
        }
    }
}
