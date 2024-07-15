using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class LinkBetterOpenURLLibraryToXCodeProject
{
    private const string libUnityAssetDatabaseGuid = "82131be8884f740a6ae03ab5637327e3";

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS)
        {
            string projectPath = PBXProject.GetPBXProjectPath(path);
            PBXProject project = new PBXProject();
            project.ReadFromFile(projectPath);

            string targetName = project.GetUnityMainTargetGuid();

            // Add the static library
            var libPath = AssetDatabase.GUIDToAssetPath(libUnityAssetDatabaseGuid);
            Debug.Log("libPath: " + libPath);
            project.AddFileToBuild(targetName, project.AddFile(libPath, "libBetterOpenURL.a"));

            project.WriteToFile(projectPath);
        }
    }
}
