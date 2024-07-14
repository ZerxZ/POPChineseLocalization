﻿using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization
{
    public static class TranslateExtension
    {
        public static bool TryTranslate(this string text, string prefix, out string translated)
        {
            POPChineseMain.LogInfo($"[{prefix}Translate] Untranslated: {text}");
            if (!AutoTranslator.Default.TryTranslate(text, out translated))
            {
                if (string.IsNullOrWhiteSpace(prefix))
                {
                    prefix = "translate";
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    POPChineseMain.GetTranslationCache(prefix).AddTranslationToCache(text, text);
                }
                return false;
            }
            POPChineseMain.LogInfo($"[{prefix}Translate] Translated: {translated}");
            return true;
        }
        public static bool TryTranslate(this string text, out string translated)
        {
            return TryTranslate(text, "", out translated);
        }
    }
}