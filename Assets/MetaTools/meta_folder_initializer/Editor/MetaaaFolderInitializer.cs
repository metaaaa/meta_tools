using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class MetaaaFolderInitializer : ScriptableWizard
{
    public string baseFolderName = "__WorkSpace";
    public string[] folders = {
        "Animations",
        "AnimatorControllers",
        "Editors",
        "Textures",
        "Materials",
        "Prefabs",
        "Resources",
        "Models",
        "Scripts",
        "Scenes",
        "Shaders",
        "Shaders/cgincs",
    };

    [MenuItem("metaaa/folder_initialize")]
    static void Init()
    {
        DisplayWizard<MetaaaFolderInitializer>("FolderInitializer");
    }

    void OnWizardCreate()
    {
        if (AssetDatabase.IsValidFolder("Assets/"+baseFolderName))
        {
            return;
        }

        foreach (string folder in folders)
        {
            FolderName(folder);
        }

        var cgincs = Directory.GetFiles(MetaToolsEnv.GetMetaToolsRelativePath("meta_shader_functions/Cgincs/"), "*.cginc", SearchOption.AllDirectories);
        foreach (string cginc in cgincs)
        {
            string fileName = cginc.Split('/').Last();
            CopyCginc(fileName);
        }

    }

    void FolderName(string folderName)
    {
        string path = "Assets/"+baseFolderName+"/" + folderName;

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetDatabase.ImportAsset(path);
    }

    void CopyCginc(string cgincName)
    {
        string source = MetaToolsEnv.GetMetaToolsRelativePath("meta_shader_functions/Cgincs/" + cgincName);
        string dest = "Assets/"+baseFolderName+"/" + "Shaders/cgincs/" + cgincName;
        FileUtil.CopyFileOrDirectory(source, dest);
        AssetDatabase.ImportAsset(dest);
    }
}
