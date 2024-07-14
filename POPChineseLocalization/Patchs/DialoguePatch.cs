using System.Collections.Generic;
using HarmonyLib;

namespace POPChineseLocalization.Patch
{
    [HarmonyPatch]
    public static class DialoguePatch
    {
        [HarmonyPatch(typeof(Dialogue),MethodType.Constructor,typeof(string))]
        [HarmonyPrefix]
        public static void Dialogue_Prefix(ref string s)
        {
            if (!s.TryTranslate("Dialogue",out var translated)) return;
            s = translated;
        }
        [HarmonyPatch(typeof(Dialogue),MethodType.Constructor,typeof(string),typeof(Dialogue.Answer))]
        [HarmonyPrefix]
        public static void Dialogue_Prefix(ref string s,ref  Dialogue.Answer a)
        {
            if (s.TryTranslate("Dialogue", out var translated))
            {
                s = translated;
            }
            if (a.text.TryTranslate( "Dialogue",out var translated2))
            {
                a.text = translated2;
            }
            if (a.reqFailedText.TryTranslate( "Dialogue",out var translated3))
            {
                a.reqFailedText = translated3;
            }
        }
        [HarmonyPatch(typeof(Dialogue),MethodType.Constructor,typeof(string),typeof(List<Dialogue.Answer>))]
        [HarmonyPrefix]
        public static void Dialogue_Prefix(ref string s,ref  List<Dialogue.Answer> a)
        {
            if (s.TryTranslate("Dialogue", out var translated))
            {
                s = translated;
            }
            foreach (var answer in a)
            {
                if (answer.text.TryTranslate( "Dialogue",out var translated2))
                {
                    answer.text = translated2;
                }
                if (answer.reqFailedText.TryTranslate( "Dialogue",out var translated3))
                {
                    answer.reqFailedText = translated3;
                }
            }
        }
    }
}