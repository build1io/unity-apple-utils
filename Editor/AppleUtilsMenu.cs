#if UNITY_EDITOR

using UnityEditor;

namespace Build1.UnityAppleUtils.Editor
{
    internal static class AppleUtilsMenu
    {
        [MenuItem("Tools/Build1/Apple Utils/Setup...", false, 1)]
        public static void Setup()
        {
            AppleUtilsProcessor.Setup();
        }
    }
}

#endif