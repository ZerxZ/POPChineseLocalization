using HarmonyLib;
using TMPro;
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
            if (!text.TryTranslate("ToolTipManagerShowTooltip",out var translated)) return;
            text = translated;
        }
        [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.addTooltipTo))]
        [HarmonyPrefix]
        public static void addTooltipTo_Prefix(GameObject g,ref string text, ToolTipManager.Position p )
        {
            if (!text.TryTranslate("ToolTipManagerAddTooltipTo",out var translated)) return;
            text = translated;
        }
        [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.showCharacterTooltip), typeof(Stats), typeof(bool))]
        [HarmonyPostfix]
        public static void showCharacterTooltip_Postfix(ref ToolTipManager __instance,Stats s, bool defaultPosition )
        {
            if (__instance.characterName.text.TryTranslate("ToolTipManagerShowCharacterTooltip",out var translated))
            {
                __instance.characterName.text  = translated;
            }

            foreach (var tmpText in __instance.stats)
            {
                if (tmpText.text.TryTranslate("ToolTipManagerShowCharacterTooltip",out  translated))
                {
                    tmpText.text = translated;
                }
            }
            foreach (var geneticStatGo in __instance.geneticStats)
            {
                var tmpText = geneticStatGo.GetComponent<TMP_Text>();
                if (tmpText is null)
                {
                    continue;
                }
                if (tmpText.text.TryTranslate("ToolTipManagerShowCharacterTooltip",out  translated))
                {
                    tmpText.text = translated;
                }
            }
        }
        // [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.getTextForAction))]
        // [HarmonyPostfix]
        // public static void getTextForAction_Postfix(OWInterface.ActionButtons a ,ref string __result)
        // {
        //     if (!__result.TryTranslate("ToolTipManager_getTextForAction",out var translated)) return;
        //     __result = translated;
        // }
        // [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.getTextForPartySpecial))]
        // [HarmonyPostfix]
        // public static void getTextForPartySpecial_Postfix(Party.PartyTraits p ,ref string __result)
        // {
        //     if (!__result.TryTranslate("ToolTipManager_getTextForPartySpecial",out var translated)) return;
        //     __result = translated;
        // }
    }
}