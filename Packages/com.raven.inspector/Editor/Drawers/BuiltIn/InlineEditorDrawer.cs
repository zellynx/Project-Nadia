using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn
{
    [CustomRavenDrawer(
        typeof(InlineEditorMetadata))]
    public sealed class
        InlineEditorDrawer
        : IRavenDrawer
    {
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Value;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Structural;

        public int Priority => 500;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is InlineEditorMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(
            RavenDrawerContext context) {
            var property =
                context.Property;

            if (property == null)
            {
                return;
            }

            var root =
                new VisualElement();

            root.style.flexDirection =
                FlexDirection.Column;

            context.RenderParent
                .Add(root);

            var propertyField =
                new PropertyField(property);

            root.Add(propertyField);

            propertyField.Bind(
                context.SerializedObject);

            propertyField.RegisterValueChangeCallback(
                _ =>
                {
                    RebuildInline(
                        root,
                        property);
                });

            RebuildInline(
                root,
                property);
        }

        private static void
            RebuildInline(
                VisualElement root,
                SerializedProperty property)
        {
            string containerName =
                $"inline-{property.propertyPath}";

            var existing =
                root.Q<VisualElement>(
                    containerName);

            existing?.RemoveFromHierarchy();

            if (property.objectReferenceValue
                == null)
            {
                return;
            }

            var inlineContainer =
                new VisualElement();

            inlineContainer.name =
                containerName;

            inlineContainer.style.marginLeft =
                16;

            inlineContainer.style.marginTop =
                4;

            var editor =
                UnityEditor.Editor
                    .CreateEditor(
                        property
                            .objectReferenceValue);

            if (editor == null)
            {
                return;
            }

            var inspector =
                new InspectorElement(
                    editor);

            inlineContainer.Add(
                inspector);

            root.Add(inlineContainer);
        }
    }
}