using BepInEx;
using HarmonyLib;

namespace POPChineseLocalization
{
    [BepInPlugin("popchineselocalization.plugin", "POPChineseLocalization", "1.0.0")]
    public class POPChineseMain : BaseUnityPlugin
    {
        public static POPChineseMain I                      { get; private set; }
        public static void           LogInfo(object    obj) => I.Logger.LogInfo(obj);
        public static void           LogWarning(object obj) => I.Logger.LogWarning(obj);
        public static void           LogError(object   obj) => I.Logger.LogError(obj);
        public static void           LogDebug(object   obj) => I.Logger.LogDebug(obj);
        public static void           LogFatal(object   obj) => I.Logger.LogFatal(obj);
        public static void           LogMessage(object obj) => I.Logger.LogMessage(obj);
        private       Harmony        _harmony;
        private void Awake()
        {
            _harmony = new Harmony("popchineselocalization.plugin");
            _harmony.PatchAll();
            I = this;
            LogInfo("POPChineseLocalization loaded!");
        }
    }
}