using UnityEditor;

namespace Editor.Utility
{
    public static class
        RavenSerializedPropertyUtility
    {
        public static object GetValue(
            SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return property.intValue;

                case SerializedPropertyType.Boolean:
                    return property.boolValue;

                case SerializedPropertyType.Float:
                    return property.floatValue;

                case SerializedPropertyType.String:
                    return property.stringValue;

                case SerializedPropertyType.Enum:
                    return property.enumValueIndex;

                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue;
            }

            return null;
        }
    }
}