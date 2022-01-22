using System;
using System.Collections.Generic;

using HarmonyLib;
using RimWorld;
using Verse;

namespace Zenth_MergelessStocks
{
    [HarmonyPatch(typeof(StorageSettings), nameof(StorageSettings.ExposeData))]
    public static class MergeSettings
    {
        //Why does readonly work???
        public static readonly HashSet<StorageSettings> noMerge = new HashSet<StorageSettings>();

        public static void Postfix(StorageSettings __instance)
        {
            if (Scribe.mode == LoadSaveMode.Saving)
            {
                if (!__instance.IsMerging()) Scribe.saver.WriteElement("noMerging", 1.ToString());
            }
            else if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                if( Convert.ToBoolean(ScribeExtractor.ValueFromNode(Scribe.loader.curXmlParent["noMerging"], 0)) )
                {
                    __instance.SetNoMerge();
                }
            }
        }

        //Extension methods
        public static bool IsMerging(this StorageSettings settings) => !noMerge.Contains(settings);
        public static void SetNoMerge(this StorageSettings settings) => noMerge.Add(settings);
        public static void SetMerge(this StorageSettings settings) => noMerge.Remove(settings);
    }
}
