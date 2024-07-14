using HarmonyLib;
using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(Date), nameof(Date.addMessage))]
    public static class DateAddMessagePatch
    {
        public static void Prefix(ref string message,bool paragraph )
        {
            if (!message.TryTranslate(nameof(DateAddMessagePatch),out var translated)) return;
            message = translated;
        }
    }
}