namespace MetaTools
{
#if UNITY_EDITOR
using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEditor;

public static class DirectlyUtils
{
    /// <summary>
    /// フォルダの取得。フォルダが無かったらフォルダを作成
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetOrCreateFolder(string path)
    {
        string folderPath;
        if (!IsFolderExists(path))
        {
            folderPath = CreateFolder(path);
        }
        else
        {
            folderPath = ConvertToAssetPath(path);
        }

        return folderPath;
    }

    public static bool IsFolderExists(string path)
    {
        return Directory.Exists(ConvertToFullPath(path));
    }

    /// <summary>
    /// フルパスに変換
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ConvertToFullPath(string path)
    {
        return Path.Combine(Application.dataPath, path);
    }

    /// <summary>
    /// Assetsで始まる形式のフォルダパスに変換
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ConvertToAssetPath(string path)
    {
        return Path.Combine("Assets", path);
    }

    /// <summary>
    /// フォルダ作成
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string CreateFolder(string path)
    {
        var folderPath = ConvertToAssetPath(path);
        Directory.CreateDirectory(folderPath);
        AssetDatabase.ImportAsset(folderPath);

        return folderPath;
    }

}
#endif
}
