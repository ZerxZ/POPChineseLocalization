using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(PopUpController),nameof(PopUpController.getPopUpText))]
    public static class PopUpControllerPatch
    {
        public static void Postfix(ref LoadingManager __instance,ref string __result)
        {
            
            if (!__result.TryTranslate("PopUpController",out var translated)) return;
            __result = translated;
        }
    }
}