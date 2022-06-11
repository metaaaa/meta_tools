namespace MetaTools
{
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MetaToolsEnv : MonoBehaviour
{
    public static bool IS_PACKAGE_RELEASE = true;

    const string PACKAGE_PATH = "Packages/com.metaaaa.meta_tools/";

    // パッケージ版か開発環境のパスを返す
    public static string GetMetaToolsPath(string path)
    {
        string absolute = "";
        if(IS_PACKAGE_RELEASE)
            absolute = Path.GetFullPath(PACKAGE_PATH + path);
        else
            absolute = Application.dataPath + "/MetaTools/" + path;
        return absolute;
    }

    // パッケージ版か開発環境のパスを返す
    public static string GetMetaToolsRelativePath(string path)
    {
        string relative = "";
        if(IS_PACKAGE_RELEASE)
            relative = PACKAGE_PATH + path;
        else
            relative = "Assets/MetaTools/" + path;
        return relative;
    }
}
#endif
}
