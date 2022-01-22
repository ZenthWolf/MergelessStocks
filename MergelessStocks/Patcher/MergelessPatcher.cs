using HarmonyLib;
using Verse;

namespace Zenth_MergelessStocks
{
    [StaticConstructorOnStartup]
    public static class MergelessMain
    {
        static MergelessMain()
        {
            var harmony = new Harmony("ZenthWolf.MergelessStocks");
            harmony.PatchAll();
        }
    }
}
