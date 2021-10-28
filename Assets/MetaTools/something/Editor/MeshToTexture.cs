namespace MetaTools
{
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class MeshToTexture : ScriptableWizard
{
    public Mesh mesh;
    public float scale = 1;
    public Vector3 rotation = new Vector3(0,0,0);
    public string fileName = "";
    public string filePath = "__WorkSpace/Textures";
    public ComputeShader infoTexGen;
    public struct MeshInfo
    {
        public Vector3 position;
        public Vector3 normal;
        public Vector3 tangent;
        public Vector2 uv;
    }
    public struct PolyInfo
    {
        public Vector3 polygon;
    }


    [MenuItem("metaaa/something/MeshToTexture")]
    static void Init()
    {
        DisplayWizard<MeshToTexture>("MeshToTexture");
    }

    /// <summary>
    /// Createボタンが押された
    /// </summary>
    private void OnWizardCreate()
    {
        var vCount = mesh.vertices.Length;
        var triCount = mesh.triangles.Length;
        var texWidth = Mathf.NextPowerOfTwo(vCount);
        var triTexWidth = Mathf.NextPowerOfTwo(triCount/3);

        var pRt = new RenderTexture(texWidth, 1, 0, RenderTextureFormat.ARGBHalf);
        pRt.name = string.Format("{0}.posTex", fileName);
        var nRt = new RenderTexture(texWidth, 1, 0, RenderTextureFormat.ARGBHalf);
        nRt.name = string.Format("{0}.normTex", fileName);
        var uvRt = new RenderTexture(texWidth, 1, 0, RenderTextureFormat.ARGB32);
        uvRt.name = string.Format("{0}.uvTex", fileName);
        var tRt = new RenderTexture(texWidth, 1, 0, RenderTextureFormat.ARGBHalf);
        tRt.name = string.Format("{0}.tanTex", fileName);
        var triRt = new RenderTexture(triTexWidth, 1, 0, RenderTextureFormat.ARGBHalf);
        triRt.name = string.Format("{0}.polyTex", fileName);
        foreach (var rt in new[] { pRt, nRt, uvRt, tRt, triRt})
        {
            rt.filterMode = FilterMode.Point;
            rt.enableRandomWrite = true;
            rt.Create();
            RenderTexture.active = rt;
            GL.Clear(true, true, Color.clear);
        }
        var verexArry = mesh.vertices;
        var normalArry = mesh.normals;
        var uvArray = mesh.uv;
        var tangentArray = mesh.tangents;
        var triArray = mesh.triangles;

        var infoList = new List<MeshInfo>();
        infoList.AddRange(Enumerable.Range(0, vCount)
                    .Select(idx => new MeshInfo()
                    {
                        position = verexArry[idx],
                        normal = normalArry[idx],
                        tangent = tangentArray[idx],
                        uv = uvArray[idx],
                    })
                );

        var polyArray = new Vector3[triTexWidth];
        for(var i=0; i<triCount/3; i++){
            int offset = i*3;
            polyArray[i] = new Vector3(
                (float)triArray[offset]   ,
                (float)triArray[offset+1] ,
                (float)triArray[offset+2]
                );
        }

        var polyInfoList = new List<PolyInfo>();
        polyInfoList.AddRange(Enumerable.Range(0, polyArray.Length)
                    .Select(idx => new PolyInfo()
                    {
                        polygon = polyArray[idx],
                    })
                );


        var buffer = new ComputeBuffer(infoList.Count, System.Runtime.InteropServices.Marshal.SizeOf(typeof(MeshInfo)));
        buffer.SetData(infoList.ToArray());

        var kernel = infoTexGen.FindKernel("CSMain");
        uint x, y, z;
        infoTexGen.GetKernelThreadGroupSizes(kernel, out x, out y, out z);

        infoTexGen.SetInt("VertCount", vCount);
        infoTexGen.SetFloat("Scale", scale);
        infoTexGen.SetVector("Rotation", rotation);
        infoTexGen.SetBuffer(kernel, "Info", buffer);
        infoTexGen.SetTexture(kernel, "OutPosition", pRt);
        infoTexGen.SetTexture(kernel, "OutNormal", nRt);
        infoTexGen.SetTexture(kernel, "OutTangent", tRt);
        infoTexGen.SetTexture(kernel, "OutUv", uvRt);
        infoTexGen.Dispatch(kernel, vCount / (int)x + 1, 1, 1);

        buffer.Release();

        var polyBuffer = new ComputeBuffer(polyInfoList.Count, System.Runtime.InteropServices.Marshal.SizeOf(typeof(PolyInfo)));
        polyBuffer.SetData(polyInfoList.ToArray());

        var polyKernel = infoTexGen.FindKernel("PolyTex");
        uint x2, y2, z2;
        infoTexGen.GetKernelThreadGroupSizes(polyKernel, out x2, out y2, out z2);
        infoTexGen.SetBuffer(polyKernel, "polyInfo", polyBuffer);
        infoTexGen.SetTexture(polyKernel, "OutPoly", triRt);
        infoTexGen.SetInt("TexWidth", texWidth);
        infoTexGen.Dispatch(polyKernel, polyArray.Length / (int)x2 + 1, 1, 1);

        polyBuffer.Release();

#if UNITY_EDITOR
        var folderPath = Path.Combine("Assets", filePath);
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets", filePath);

        var subFolder = name;
        var subFolderPath = Path.Combine(folderPath, subFolder);
        if (!AssetDatabase.IsValidFolder(subFolderPath))
            AssetDatabase.CreateFolder(folderPath, subFolder);

        var posTex = RenderTextureToTexture2D.Convert(pRt);
        var normTex = RenderTextureToTexture2D.Convert(nRt);
        var tanTex = RenderTextureToTexture2D.Convert(tRt);
        var uvTex = RenderTextureToTexture2D.Convert(uvRt);
        var triTex = RenderTextureToTexture2D.Convert(triRt);
        posTex.filterMode = FilterMode.Point;
        normTex.filterMode = FilterMode.Point;
        tanTex.filterMode = FilterMode.Point;
        uvTex.filterMode = FilterMode.Point;
        triTex.filterMode = FilterMode.Point;
        Graphics.CopyTexture(pRt, posTex);
        Graphics.CopyTexture(nRt, normTex);
        Graphics.CopyTexture(tRt, tanTex);
        Graphics.CopyTexture(uvRt, uvTex);
        Graphics.CopyTexture(triRt, triTex);

        AssetDatabase.CreateAsset(posTex, Path.Combine(subFolderPath, pRt.name + ".asset"));
        AssetDatabase.CreateAsset(normTex, Path.Combine(subFolderPath, nRt.name + ".asset"));
        AssetDatabase.CreateAsset(tanTex, Path.Combine(subFolderPath, tRt.name + ".asset"));
        AssetDatabase.CreateAsset(uvTex, Path.Combine(subFolderPath, uvRt.name + ".asset"));
        AssetDatabase.CreateAsset(triTex, Path.Combine(subFolderPath, triRt.name + ".asset"));

#endif
    }
}
}