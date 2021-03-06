namespace MetaTools
{
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
    [CustomEditor(typeof(CircleObject))]
    public class CircleObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var component = target as CircleObject;
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