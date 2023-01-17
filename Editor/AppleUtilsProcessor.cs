#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityAppleUtils.Editor
{
    [InitializeOnLoad]
    internal static class AppleUtilsProcessor
    {
        private const string ConfigPath = "Build1/build1-apple-utils.json";

        public static AppleUtilsConfiguration Config { get; private set; }

        private static string _configPath;

        static AppleUtilsProcessor()
        {
            EditorApplication.delayCall += Initialize;
        }

        private static void Initialize()
        {
            _configPath = Path.Combine(Application.dataPath, ConfigPath);
            
            Config = LoadConfig();
            if (Config == null)
                Setup();
        }

        /*
         * Public.
         */

        public static void Setup()
        {
            var config = new AppleUtilsConfiguration
            {
                appUsesNonExemptEncryption = EditorUtility.DisplayDialog("Unity Apple Utils", "Does this app uses any kind of non extent encryption?", "Yes, Enable Encryption Check", "No, Disable Encryption Check"),
                bitCodeEnabled = EditorUtility.DisplayDialog("Unity Apple Utils", "Do you want to leave bitcode generation enabled?\n\n(XCode 14 and above doesn't support bitcode generation)", "Yes, Leave Default Bitcode Settings", "No, Disable BitCode Generation for All Targets")
            };

            Config = config;

            var json = JsonUtility.ToJson(Config, true);
            File.WriteAllText(_configPath, json);

            Debug.Log("Apple Utils: Settings updated");
        }

        /*
         * Private.
         */

        private static AppleUtilsConfiguration LoadConfig()
        {
            if (!File.Exists(_configPath))
                return null;

            var json = File.ReadAllText(_configPath);
            return JsonUtility.FromJson<AppleUtilsConfiguration>(json);
        }
    }
}

#endif