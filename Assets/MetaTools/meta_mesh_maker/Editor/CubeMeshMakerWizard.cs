namespace MetaTools
{
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class CubeMeshMakerWizard : ScriptableWizard
{
    public string filename = "mesh";
    public string path = "Assets/__WorkSpace/Models/";

    public int xnum = 10;
    public int ynum = 10;
    public int znum = 10;

    public Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);

    [MenuItem("metaaa/mesh maker/Cube Mesh Maker")]
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

        Mesh mesh = meshMaker.GenerageMesh(xnum, ynum, znum, scale);

        AssetDatabase.CreateAsset(mesh, path + filename + ".asset");
        AssetDatabase.SaveAssets();
    }
}
#endif
}
