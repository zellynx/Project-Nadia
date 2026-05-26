using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Fields
{
    public static class
        RavenFieldFactory
    {
        public static RavenFieldResult
            Create(
                SerializedProperty property,
                string label,
                Action onChanged)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                {
                    var field =
                        new IntegerField(label);

                    field.BindProperty(property);

                    return new RavenFieldResult
                    {
                        Element = field,

                        RegisterCallbacks = () =>
                        {
                            field.RegisterValueChangedCallback(
                                _ => onChanged());
                        }
                    };
                }

                case SerializedPropertyType.Float:
                {
                    var field =
                        new FloatField(label);

                    field.BindProperty(property);

                    return new RavenFieldResult
                    {
                        Element = field,

                        RegisterCallbacks = () =>
                        {
                            field.RegisterValueChangedCallback(
                                _ => onChanged());
                        }
                    };
                }

                case SerializedPropertyType.String:
                {
                    var field =
                        new TextField(label);

                    field.BindProperty(property);

                    return new RavenFieldResult
                    {
                        Element = field,

                        RegisterCallbacks = () =>
                        {
                            field.RegisterValueChangedCallback(
                                _ => onChanged());
                        }
                    };
                }

                case SerializedPropertyType.Boolean:
                {
                    var field =
                        new Toggle(label);

                    field.BindProperty(property);

                    return new RavenFieldResult
                    {
                        Element = field,

                        RegisterCallbacks = () =>
                        {
                            field.RegisterValueChangedCallback(
                                _ => onChanged());
                        }
                    };
                }

                case SerializedPropertyType.Enum:
                {
                    var field =
                        new EnumField(label);

                    field.BindProperty(property);

                    return new RavenFieldResult
                    {
                        Element = field,

                        RegisterCallbacks = () =>
                        {
                            field.RegisterValueChangedCallback(
                                _ => onChanged());
                        }
                    };
                }
            }

            return null;
        }
    }
}