namespace MetaTools
{
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
    [CustomEditor(typeof(CurveTextureObject))]
    public class CurveTextureEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var component = target as CurveTextureObject;
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