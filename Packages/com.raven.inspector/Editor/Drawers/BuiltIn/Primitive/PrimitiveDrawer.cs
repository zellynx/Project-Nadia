using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using Reflection;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Primitive
{
    public sealed class PrimitiveDrawer : IRavenDrawer
    {
        public int Priority => 0;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Value;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Structural;

        public bool CanDraw(RavenDrawerContext context)
        {
            return true;
        }

        public void Draw(RavenDrawerContext context)
        {
            if (context.Node.Metadata.MethodInfo
                != null)
            {
                return;
            }
            
            var type =
                context.Node.Metadata.PropertyType;

            if (!RavenTypeUtility.IsPrimitiveLike(type))
            {
                if (!type.IsEnum)
                {
                    return;
                }
            }
            
            var fieldInfo =
                context.Node.Metadata.FieldInfo;

            if (fieldInfo == null)
            {
                return;
            }

            var property =
                context.SerializedObject.FindProperty(
                    context.Node.Metadata.SerializedPath);

            if (property == null)
            {
                return;
            }

            var propertyField =
                new PropertyField(property);

            propertyField.label =
                context.Node.Metadata.Name;

            context.CurrentElement =
                propertyField;

            VisualElement targetContainer =
                context.RenderParent;

            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata is not GroupMetadata group)
                {
                    continue;
                }

                if (context.Groups.TryGetGroup(
                        group.GroupName,
                        out var groupContainer))
                {
                    targetContainer =
                        groupContainer;
                }
            }

            targetContainer.Add(propertyField);
            
            propertyField.RegisterValueChangeCallback(
                _ =>
                {
                    context.Dependencies.Notify(
                        context.Node.Metadata.Name);
                });
        }
    }
}