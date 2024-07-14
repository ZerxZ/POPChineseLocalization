using System.Collections.Generic;
using System.IO;
using BepInEx;
using HarmonyLib;
using TMPro;
using UnityEngine;
using XUnity.AutoTranslator.Plugin.Core;

namespace POPChineseLocalization
{
    [BepInPlugin("popchineselocalization.plugin", "POPChineseLocalization", "1.0.0")]
    public class POPChineseMain : BaseUnityPlugin
    {
        public static POPChineseMain                                 I                      { get; private set; }
        public static void                                           LogInfo(object    obj) => I.Logger.LogInfo(obj);
        public static void                                           LogWarning(object obj) => I.Logger.LogWarning(obj);
        public static void                                           LogError(object   obj) => I.Logger.LogError(obj);
        public static void                                           LogDebug(object   obj) => I.Logger.LogDebug(obj);
        public static void                                           LogFatal(object   obj) => I.Logger.LogFatal(obj);
        public static void                                           LogMessage(object obj) => I.Logger.LogMessage(obj);
        private       Harmony                                        _harmony;
        public static Dictionary<string, SimpleTextTranslationCache> TranslationCache = new Dictionary<string, SimpleTextTranslationCache>();
        public static string                                         LanguageDir { get; private set; }
        public static string                                         TextDir     => Path.Combine(LanguageDir, "Text");
        private void Awake()
        {
            _harmony = new Harmony("popchineselocalization.plugin");
            _harmony.PatchAll();
            I = this;
            LogInfo("POPChineseLocalization loaded!");
            LanguageDir = Path.GetDirectoryName(AutoTranslatorSettings.DefaultRedirectedResourcePath);
            AutoTranslator.Default.RegisterOnTranslatingCallback(OnTranslating);
        }
        private void OnTranslating(ComponentTranslationContext obj)
        {
            var isTranslated = !string.IsNullOrWhiteSpace(obj.OverriddenTranslatedText);
            var objName      = "";
            // LogInfo($"[Translate] Untranslated: {typeof(obj.OriginalText)}");
            switch (obj.Component)
            {
                case MonoBehaviour mb:
                    objName = mb.name;

                    break;
                case Component c:
                    objName = c.name;
                    break;
                case GameObject go:
                    objName = go.name;
                    break;
                default:
                    objName = obj.Component.GetType().Name;
                    break;
            }

            GetTranslationCache(objName, isTranslated).AddTranslationToCache(obj.OriginalText, isTranslated ? objName : obj.OriginalText);
        }
        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }
        public static SimpleTextTranslationCache GetTranslationCache(string key, bool translated = false)
        {
            if (TranslationCache.TryGetValue(key, out var cache))
            {
                return cache;
            }
            var untranslatedDir = Path.Combine(TextDir, translated ? "Translated" : "Untranslated");
            if (!Directory.Exists(untranslatedDir))
            {
                Directory.CreateDirectory(untranslatedDir);
            }
            var cacheFile = Path.Combine(untranslatedDir, $"{key}.txt");
            if (!File.Exists(cacheFile))
            {
                File.WriteAllText(cacheFile, "");
            }
            cache = new SimpleTextTranslationCache(cacheFile, true);
            TranslationCache[key] = cache;
            return cache;
        }
    }
}