namespace MetaTools
{
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ShaderTempleteExtension : MonoBehaviour
{
    // [MenuItem("Assets/Create/Shader/CustomRenderTextureShader")]
    // private static void CreateNewAsset()
    // {
    //     string path = "D:/Projects/CRTBoids/Assets/Shaders/Boxcel.shader";

    //     string data = File.ReadAllText(path);

    //     // unity 2018.2.5f1 以上
    //     ProjectWindowUtil.CreateAssetWithContent(
    //         "NewCRTShader.shader",
    //         data);
    // }

    [MenuItem("Assets/Create/Shader/Custom Render Texture Init Shader")]
    private static void CreateCRTInitShader()
    {
        string path = MetaToolsEnv.GetMetaToolsPath("meta_shader_functions/Templetes/CRTInitTemplete.shader.templete");
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewCRTInitShader.shader");
    }

    [MenuItem("Assets/Create/Shader/Custom Render Texture Update Shader")]
    private static void CreateCRTUpdateShader()
    {
        string path = MetaToolsEnv.GetMetaToolsPath("meta_shader_functions/Templetes/CRTUpdateTemplete.shader.templete");
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewCRTUpdateShader.shader");
    }
}
}