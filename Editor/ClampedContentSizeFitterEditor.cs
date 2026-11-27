#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using MUD.Modules.ClampedContentSizeFitter;

namespace MUD.Modules.ClampedContentSizeFitter.Editor
{
    [CustomEditor(typeof(ClampedContentSizeFitter))]
    public class ClampedContentSizeFitterEditor : ContentSizeFitterEditor
    {
        private SerializedProperty m_HorizontalFit;
        private SerializedProperty m_VerticalFit;
        private SerializedProperty m_MinWidth;
        private SerializedProperty m_MaxWidth;
        private SerializedProperty m_MinHeight;
        private SerializedProperty m_MaxHeight;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_HorizontalFit = serializedObject.FindProperty("m_HorizontalFit");
            m_VerticalFit = serializedObject.FindProperty("m_VerticalFit");
            m_MinWidth = serializedObject.FindProperty("m_MinWidth");
            m_MaxWidth = serializedObject.FindProperty("m_MaxWidth");
            m_MinHeight = serializedObject.FindProperty("m_MinHeight");
            m_MaxHeight = serializedObject.FindProperty("m_MaxHeight");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            if (m_HorizontalFit.enumValueIndex != 0)
            {
                ToggleFloatField(m_MinWidth, 0, new GUIContent("Min Width"));
                ToggleFloatField(m_MaxWidth, 100, new GUIContent("Max Width"));
            }

            if (m_VerticalFit.enumValueIndex != 0)
            {
                ToggleFloatField(m_MinHeight, 0, new GUIContent("Min Height"));
                ToggleFloatField(m_MaxHeight, 100, new GUIContent("Max Height"));
            }

            serializedObject.ApplyModifiedProperties();
        }

        void ToggleFloatField(SerializedProperty property, float defaultValue, GUIContent rawLabel)
        {
            var position = EditorGUILayout.GetControlRect();
            var label = EditorGUI.BeginProperty(position, rawLabel, property);
            var fieldPosition = EditorGUI.PrefixLabel(position, label);
            var toggleRect = fieldPosition;
            toggleRect.width = 16.0f;
            var floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16.0f;

            EditorGUI.BeginChangeCheck();
            var enabled = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.floatValue >= 0.0f);
            if (EditorGUI.EndChangeCheck())
                property.floatValue = enabled ? defaultValue : -1;

            if (!property.hasMultipleDifferentValues && property.floatValue >= 0.0f)
            {
                float previousLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 4.0f;
                EditorGUI.BeginChangeCheck();
                var newValue = EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), property.floatValue);
                if (EditorGUI.EndChangeCheck())
                    property.floatValue = Mathf.Max(0, newValue);
                EditorGUIUtility.labelWidth = previousLabelWidth;
            }

            EditorGUI.EndProperty();
        }
    }
}

#endif
