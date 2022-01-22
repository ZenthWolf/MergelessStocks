using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Zenth_MergelessStocks
{
    [HarmonyPatch(typeof(WorkGiver_Merge), "JobOnThing")]
    internal static class WorkGiver_Mergeless
    {
        private static void Postfix(ref Job __result, Thing t)
        {
            if(__result != null)
            {
                ISlotGroupParent zone = t.GetSlotGroup().parent;
                if (zone is Zone_Stockpile)
                {
                    if( (zone as Zone_Stockpile).settings.Priority == StoragePriority.Low)
                    {
                        __result = null;
                    }
                }
            }
        }
    }
}
