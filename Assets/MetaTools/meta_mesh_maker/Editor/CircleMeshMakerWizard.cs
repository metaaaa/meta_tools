namespace MetaTools
{
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class CircleMeshMakerWizard : ScriptableWizard
{
    public string _fileName = "mesh";
    public string _exportFolder = "__WorkSpace/Models/";
    private string _meshName = "Circle";

    [MenuItem("metaaa/mesh maker/Circle Mesh Maker")]
    static void Init()
    {
        DisplayWizard<CircleMeshMakerWizard>("Circle Mesh Maker");
    }

    private string ExportPath =>
            Path.Combine(DirectlyUtils.ConvertToAssetPath(_exportFolder), $"{_fileName}.asset");


    /// <summary>
    /// Createボタンが押された
    /// </summary>
    private void OnWizardCreate()
    {
        var folderPath = DirectlyUtils.GetOrCreateFolder(_exportFolder);
        string exportPath = Path.Combine(folderPath, $"{_fileName}.asset");
        exportPath = AssetDatabase.GenerateUniqueAssetPath(exportPath);

        // CircleObject作成
        var circleObject = ScriptableObject.CreateInstance<CircleObject>();
        circleObject.CreateMesh();
        var mesh = circleObject.mesh;
        mesh.name = _meshName;
        AssetDatabase.CreateAsset(circleObject, exportPath);

        // 格納
        AssetDatabase.AddObjectToAsset(mesh, circleObject);
        AssetDatabase.SaveAssets();
        
        EditorGUIUtility.PingObject(circleObject);
    }
}
#endif
}
