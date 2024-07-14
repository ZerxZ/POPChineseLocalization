using HarmonyLib;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch(typeof(ToolTipManager), nameof(ToolTipManager.showTooltip))]
    public class ToolTipManagerPatch
    {
        public static void Prefix(ToolTipManager.Position pos, ref string text, bool showInstant )
        {
            if (!text.TryTranslate(out var translated)) return;
            text = translated;
        }
    }
}