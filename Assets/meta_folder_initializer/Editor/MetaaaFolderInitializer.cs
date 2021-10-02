using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MetaaaFolderInitializer : EditorWindow
{
    private static string baseFolderName = "__WorkSpace";

    [MenuItem("metaaa/folder_initialize")]
    static void CreateFolder()
    {

        if (AssetDatabase.IsValidFolder("Assets/"+baseFolderName))
        {
            return;
        }

        FolderName("Animations");
        FolderName("AnimatorControllers");
        FolderName("Editors");
        FolderName("Textures");
        FolderName("Materials");
        FolderName("Prefabs");
        FolderName("Resources");
        FolderName("RenderTextures");
        FolderName("Scripts");
        FolderName("Scenes");
        FolderName("Shaders");

    }


    static void FolderName(string folderName)
    {
        string path = "Assets/"+baseFolderName+"/" + folderName;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetDatabase.ImportAsset(path);
    }
}
