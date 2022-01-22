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
            
            // See how disabling this breaks something- it'll be cool!
            if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Escape))
            {
                UI.UnfocusCurrentControl();
                Event.current.Use();
            }

            var settings = selected.GetStoreSettings();

            //rect changes in method, start up from search+clear (48 px) + buffers (3) + DrawArea height (30)
            Rect drawArea = new Rect(rect.xMin, rect.yMin - 48f - 3f - 30f, rect.width, 24f);

            var checkMerge = settings.IsMerging();
            var wasMerge = checkMerge;

            TooltipHandler.TipRegion(drawArea, "This tooltip better not be why it broke");
            Widgets.CheckboxLabeled(drawArea, "Merge Stockpile", ref checkMerge, placeCheckboxNearText: true);

            if(wasMerge ^ checkMerge)
            {
                if (checkMerge) settings.SetMerge();
                else settings.SetNoMerge();
            }
        }
    }
}
