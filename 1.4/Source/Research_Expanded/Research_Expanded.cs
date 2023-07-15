using HarmonyLib;
using UnityEngine;
using Verse;

namespace Research_Expanded
{
    public class Mod : Verse.Mod
    {
        public static Settings settings;

        public Mod(ModContentPack content) : base(content)
        {
            Log.Message("Hello world from Research Expanded");

            // initialize settings
            settings = GetSettings<Settings>();

#if DEBUG
			Harmony.DEBUG = true;
#endif

            Harmony harmony = new Harmony("TheCatLover366.rimworld.Research_Expanded.main");
            harmony.PatchAll();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Research Expanded";
        }
    }
}
