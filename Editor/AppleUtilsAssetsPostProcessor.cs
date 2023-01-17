#if UNITY_EDITOR

using UnityEditor;

namespace Build1.UnityAppleUtils.Editor
{
    internal sealed class AppleUtilsAssetsPostProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!AppleUtilsProcessor.CheckIfConfigured())
                AppleUtilsProcessor.Setup();
        }
    }
}

#endif