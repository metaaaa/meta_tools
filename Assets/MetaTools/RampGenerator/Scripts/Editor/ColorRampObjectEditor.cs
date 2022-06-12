namespace MetaTools.RampTextureGenerator
{
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
    [CustomEditor(typeof(MetaTools.RampTextureGenerator.ColorRampObject))]
    public class ColorRampObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var component = target as MetaTools.RampTextureGenerator.ColorRampObject;
            if (component == null) return;

            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if(EditorGUI.EndChangeCheck()){
                component.ApplyGradient();
                EditorUtility.SetDirty(component);
            }

        }

        public override bool HasPreviewGUI()
        {
            return true;
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var component = target as MetaTools.RampTextureGenerator.ColorRampObject;
            if (component == null) return;
            if (component.Texture == null) return;
            
            GUI.DrawTexture(r, component.Texture);
        }
    }

#endif
}