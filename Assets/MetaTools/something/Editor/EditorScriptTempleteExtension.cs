namespace MetaTools
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScriptTempleteExtension : MonoBehaviour
{
    [MenuItem("Assets/Create/Editor/Scriptable Wizard Script")]
    private static void CreateScriptableWizardScript()
    {
        string path = MetaToolsEnv.GetMetaToolsPath( "something/Templetes/ScriptableWizardTemplete.cs.templete");
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewScriptableWizard.cs");
    }
}
}
