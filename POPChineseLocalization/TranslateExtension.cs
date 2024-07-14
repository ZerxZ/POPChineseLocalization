using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization
{
    public static class TranslateExtension
    {
        public static bool TryTranslate(this string text, out string translated)
        {
            POPChineseMain.LogInfo($"Translate message:{text}"); 
            if (!AutoTranslator.Default.TryTranslate(text, out  translated)) return false;
            POPChineseMain.LogInfo($"Translate translated:{translated}"); 
            return true;
        }
    }
}