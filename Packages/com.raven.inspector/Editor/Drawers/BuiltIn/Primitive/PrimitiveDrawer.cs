using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Editor.Fields;
using Metadata.Models;
using Reflection;
using UnityEditor;
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
                context.Property;

            if (property == null)
            {
                return;
            }

            var fieldResult =
                RavenFieldFactory.Create(
                    property,
                    context.Node.Metadata.Name,
                    () =>
                    {
                        context.Dependencies.Notify(
                            context.Node.Metadata.Name);
                    });

            if (fieldResult == null)
            {
                return;
            }

            var fieldElement = fieldResult.Element;

            fieldResult.RegisterCallbacks?.Invoke();

            if (fieldElement == null)
            {
                return;
            }

            fieldElement.style.flexGrow = 1;

            context.CurrentElement =
                fieldElement;

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

            var wrapper =
                new VisualElement();
            
            wrapper.AddToClassList(
                "raven-horizontal-item");

            wrapper.style.flexGrow = 1;

            wrapper.Add(fieldElement);

            targetContainer.Add(wrapper);
            
            
        }
    }
}