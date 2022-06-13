namespace MetaTools
{
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
    [CustomEditor(typeof(CubeObject))]
    public class CubeObjectEditorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var component = target as CubeObject;
            if (component == null) return;

            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if(EditorGUI.EndChangeCheck()){
                component.OnParamUpdated();
                EditorUtility.SetDirty(component);
            }
        }
    }

#endif
}