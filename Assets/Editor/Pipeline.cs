using System;
using System.IO;
using System.Linq;
using UnityEngine;
namespace UnityEditor
{
    public class Pipeline
    {
        [MenuItem("Pipeline/Build: Android")]
        public static void BuildAndroid()
        {
            if (!isTestsPass())
            {
                EditorApplication.Exit(1);
            }
            string result = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                locationPathName = Path.Combine(pathname, filename),
                scenes = EditorBuildSettings.scenes.Where(n => n.enabled).Select(n => n.path).ToArray(),
                target = BuildTarget.Android
            }).ToString();
            Debug.Log(result);
        }

        /*
        * This is a static property which will return a string, representing a
        * build folder on the desktop. This does not create the folder when it
        * doesn't exists, it simply returns a suggested path. It is put on the
        * desktop, so it's easier to find but you can change the string to any
        * path really. Path combine is used, for better cross platform support
        */
        public static string pathname
        {
            get
            {
                string assetsPath = Application.dataPath;
                string dir = assetsPath.Substring(0, assetsPath.LastIndexOf('/')) + "/Build/Instincts";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return dir;

            }
        }

        /*
        * This returns the filename that the build should spit it. For a start
        * this just returns a current date, in a simple lexicographical format
        * with the apk extension appended. Later on, you can customize this to
        * include more information, such as last person to commit, what branch
        * were used, version of the game, or what git-hash the game were using
        */
        public static string filename
        {
            get
            {
                return ("build.apk");
            }
        }

        public static bool isTestsPass() {
            /*
             * Add test functionality here. Remember that this is called at editors
             * runtime, so you might have to use some test class mockups or keep it
             * in mind when designing your game. If any of tests fails, you need to
             * exit Unity with a non-zero return code so that Jenkins stop building
             */
            return true;
        }
    }
}