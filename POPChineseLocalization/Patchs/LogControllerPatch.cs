using HarmonyLib;
using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(LogController), nameof(LogController.addMessage), typeof(string))]
    public class LogControllerPatch
    {
        public static void Prefix(ref string message)
        {
            if (!message.TryTranslate(nameof(LogController),out var translated)) return;
            message = translated;
        }
    }
}