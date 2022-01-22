using UnityEngine;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Zenth_MergelessStocks
{
    [HarmonyPatch(typeof(ThingFilterUI), "DoThingFilterConfigWindow")]
    public class ThingFilterUIPatcher
    {
        // Will contain selected object from UI
        static ISlotGroupParent selected;

        // Reserve draw area by reducing list- consider if one can increase window size?
        public static void Prefix(ref Rect rect)
        {
            selected = Find.Selector.SingleSelectedObject as ISlotGroupParent;
            if (selected != null) rect.yMin += 30f;
        }

        // Draw new checkbox option
        public static void Postfix(ref Rect rect)
        {
            if (selected == null) return;

            var settings = selected.GetStoreSettings();

            //rect changes in method, start up from search+clear (48 px) + buffers (3) + DrawArea height (30)
            Rect drawArea = new Rect(rect.xMin, rect.yMin - 48f - 3f - 30f, rect.width, 24f);

            //Is there a timing issue??? Testing shows no, but the reading feels like you need to be extremely lucky to "catch" the checkbox at the right time...
            var checkMerge = settings.IsMerging();
            var wasMerge = checkMerge;

            TooltipHandler.TipRegion(drawArea, "Enable/Disable Automatic Merge Assignments Here");
            Widgets.CheckboxLabeled(drawArea, "Merge Stock Here", ref checkMerge, placeCheckboxNearText: true);

            if(wasMerge ^ checkMerge)
            {
                if (checkMerge) settings.SetMerge();
                else settings.SetNoMerge();
            }
        }
    }
}
