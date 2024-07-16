using UnityEditor;
using UnityEngine;

namespace TimboJimbo.BetterOpenURL.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    internal class ShowIfAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool show = GetConditionalShowAttributeResult(property);

            if (show)
                EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool show = GetConditionalShowAttributeResult(property);

            if (show)
                return EditorGUI.GetPropertyHeight(property, label);
            else
                return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool GetConditionalShowAttributeResult(SerializedProperty property)
        {
            ShowIfAttribute conditionalShowAttribute = (ShowIfAttribute)attribute;
            SerializedProperty sourcePropertyValue = FindPropertyEndsIn(property.serializedObject, conditionalShowAttribute.ConditionalSourceField);

            if (sourcePropertyValue == null)
            {
                Debug.LogWarning("Attempting to use a ShowIfAttribute but no matching SourcePropertyValue found in object: " + conditionalShowAttribute.ConditionalSourceField);
                return true;
            }

            return sourcePropertyValue.boolValue;
        }

        private SerializedProperty FindPropertyEndsIn(SerializedObject obj, string endsIn)
        {
            SerializedProperty iterator = obj.GetIterator();
            while (iterator.NextVisible(true))
            {
                if (iterator.propertyPath == endsIn || iterator.propertyPath.EndsWith("." + endsIn))
                {
                    return iterator;
                }
            }

            return null;
        }
    }
}
