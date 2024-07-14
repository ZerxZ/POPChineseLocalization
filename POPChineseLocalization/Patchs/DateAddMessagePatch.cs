using HarmonyLib;
using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(Date), nameof(Date.addMessage))]
    public static class DateAddMessagePatch
    {
        public static void Prefix(ref string message,bool paragraph )
        {
            POPChineseMain.LogInfo($"Translate message:{message}"); 
            if (!AutoTranslator.Default.TryTranslate(message, out var translated)) return;
            POPChineseMain.LogInfo($"Translate translated:{translated}"); 
            message = translated;
        }
    }
}