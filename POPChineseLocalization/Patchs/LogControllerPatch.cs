using HarmonyLib;
using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(LogController), nameof(LogController.addMessage), typeof(string))]
    public class LogControllerPatch
    {
        public static void Prefix(ref string message)
        {
            POPChineseMain.LogInfo($"Translate message:{message}"); 
            if (!AutoTranslator.Default.TryTranslate(message, out var translated)) return;
            POPChineseMain.LogInfo($"Translate translated:{translated}");
            message = translated;
        }
    }
}