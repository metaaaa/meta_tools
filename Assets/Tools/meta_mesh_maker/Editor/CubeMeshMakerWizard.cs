using UnityEngine;
using UnityEditor;

public class CubeMeshMakerWizard : ScriptableWizard
{
    public string filename = "mesh";
    public string path = "Assets/__WorkSpace/Models/";

    public int xnum = 10;
    public int ynum = 10;
    public int znum = 10;

    [MenuItem("Custom/Cube Mesh Maker")]
    static void Init()
    {
        DisplayWizard<CubeMeshMakerWizard>("Cube Mesh Maker");
    }

    /// <summary>
    /// Createボタンが押された
    /// </summary>
    private void OnWizardCreate()
    {
        CubeMeshMaker meshMaker = new CubeMeshMaker();

        Mesh mesh = meshMaker.GenerageMesh(xnum, ynum, znum);

        AssetDatabase.CreateAsset(mesh, path + filename + ".asset");
        AssetDatabase.SaveAssets();
    }
}