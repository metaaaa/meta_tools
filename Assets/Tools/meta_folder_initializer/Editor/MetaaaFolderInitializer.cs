using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MetaaaFolderInitializer : EditorWindow
{
    private const string BASE_FOLDER_NAME = "__WorkSpace";
    private static readonly string[] CGINCS = {
        "meta_function.cginc"
    };

    [MenuItem("metaaa/folder_initialize")]
    static void CreateFolder()
    {

        if (AssetDatabase.IsValidFolder("Assets/"+BASE_FOLDER_NAME))
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
        FolderName("Models");
        FolderName("Scripts");
        FolderName("Scenes");
        FolderName("Shaders");
        FolderName("Shaders/cgincs");

        foreach (string cginc in CGINCS)
        {
            CopyCginc(cginc);
        }

    }

    static void FolderName(string folderName)
    {
        string path = "Assets/"+BASE_FOLDER_NAME+"/" + folderName;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetDatabase.ImportAsset(path);
    }

    static void CopyCginc(string cgincName)
    {
        string source = "Assets/Tools/meta_shader_functions/" + cgincName;
        string dest = "Assets/"+BASE_FOLDER_NAME+"/" + "Shaders/cgincs/" + cgincName;
        FileUtil.CopyFileOrDirectory(source, dest);
        AssetDatabase.ImportAsset(dest);
    }
}
