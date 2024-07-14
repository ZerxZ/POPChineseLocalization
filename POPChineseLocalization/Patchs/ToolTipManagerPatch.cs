using HarmonyLib;
using UnityEngine;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch]
    
    public static class ToolTipManagerPatch
    {
    
        [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.showTooltip))]
        [HarmonyPrefix]
        public static void showTooltip_Prefix(ToolTipManager.Position pos, ref string text, bool showInstant )
        {
            if (!text.TryTranslate("ToolTipManager_showTooltip",out var translated)) return;
            text = translated;
        }
        [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.addTooltipTo))]
        [HarmonyPrefix]
        public static void addTooltipTo_Prefix(GameObject g,ref string text, ToolTipManager.Position p )
        {
            if (!text.TryTranslate("ToolTipManager_addTooltipTo",out var translated)) return;
            text = translated;
        }
        [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.showCharacterTooltip), typeof(Stats), typeof(bool))]
        [HarmonyPostfix]
        public static void showCharacterTooltip_Postfix(ref ToolTipManager __instance,Stats s, bool defaultPosition )
        {
            if (__instance.characterName.text.TryTranslate("ToolTipManager_showCharacterTooltip",out var translated))
            {
                __instance.characterName.text  = translated;
            }

            foreach (var tmpText in __instance.stats)
            {
                if (tmpText.text.TryTranslate("ToolTipManager_showCharacterTooltip",out  translated))
                {
                    tmpText.text = translated;
                }
            }
        }
    }
}