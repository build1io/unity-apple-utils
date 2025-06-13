#if UNITY_IOS

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Build1.UnityAppleUtils.Editor
{
    internal static class AppleUtilsPostProcessBuild
    {
        [PostProcessBuild(999)]
        private static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS)
                return;

            var config = AppleUtilsProcessor.LoadConfig();
            
            if (!config.appUsesNonExemptEncryption)
            {
                var plist = new PlistDocument();
                var filePath = Path.Combine(buildPath, "Info.plist");
                plist.ReadFromFile(filePath);

                var rootDict = plist.root;
                rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);
                plist.WriteToFile(filePath);    
            }

            if (!config.bitCodeEnabled)
            {
                var projPath = PBXProject.GetPBXProjectPath(buildPath);
 
                var project = new PBXProject();
                project.ReadFromFile(projPath);

                var targets = new List<string>
                {
                    project.GetUnityMainTargetGuid(),
                    project.TargetGuidByName(PBXProject.GetUnityTestTargetName()),
                    project.GetUnityFrameworkTargetGuid()
                };

                foreach (var target in targets)
                    project.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
 
                project.WriteToFile(projPath);
            }
        }
    }
}

#endif