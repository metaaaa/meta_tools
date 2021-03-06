namespace MetaTools.RampTextureGenerator
{
#if UNITY_EDITOR
using UnityEditor;
    public static class CustomUI
    {
        public static void TextFieldWithUndo(EditorWindow window, string label, ref string text)
        {
            EditorGUI.BeginChangeCheck();
            var s = EditorGUILayout.TextField(label, text);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(window, "Change String");
                text = s;
            }

        }
    }
#endif

}