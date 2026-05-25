using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Reflection;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Metadata.Models;

namespace Editor.Drawers.BuiltIn.Collection
{
    public sealed class CollectionDrawer : IRavenDrawer
    {
        public int Priority => 20;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Value;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Structural;
        
        public bool CanDraw(RavenDrawerContext context)
        {
            return RavenTypeUtility.IsCollection(
                context.Node.Metadata.PropertyType);
        }

        public void Draw(RavenDrawerContext context)
        {
            var foldout = new Foldout
            {
                text = context.Node.Metadata.Name,
                value = true
            };
            
            foldout.AddToClassList(
                "raven-collection");

            var content =
                new VisualElement();

            foldout.Add(content);

            context.RenderParent.Add(foldout);

            context.CurrentElement =
                foldout;

            RefreshCollection(
                context,
                content);
        }

        private void RefreshCollection(
            RavenDrawerContext context,
            VisualElement content)
        {
            content.Clear();

            context.SerializedObject.Update();

            var property =
                context.SerializedObject.FindProperty(
                    context.Node.Metadata.SerializedPath);

            if (property == null)
            {
                return;
            }

            for (int i = 0;
                 i < property.arraySize;
                 i++)
            {
                var element =
                    property.GetArrayElementAtIndex(i);

                var row =
                    new VisualElement();

                row.AddToClassList(
                    "raven-row");

                row.style.marginBottom = 2;

                var field =
                    new PropertyField(
                        element,
                        $"Element {i}");

                field.BindProperty(element);

                field.AddToClassList(
                    "raven-flex-grow");

                int capturedIndex = i;

                var removeButton =
                    new Button(() =>
                    {
                        context.SerializedObject
                            .Update();

                        var refreshed =
                            context.SerializedObject
                                .FindProperty(
                                    context.Node.Metadata
                                        .SerializedPath);

                        refreshed
                            .DeleteArrayElementAtIndex(
                                capturedIndex);

                        context.SerializedObject
                            .ApplyModifiedProperties();

                        RefreshCollection(
                            context,
                            content);
                    })
                    {
                        text = "-"
                    };

                row.Add(field);

                row.Add(removeButton);

                content.Add(row);
            }

            var addButton =
                new Button(() =>
                {
                    context.SerializedObject
                        .Update();

                    var refreshed =
                        context.SerializedObject
                            .FindProperty(
                                context.Node.Metadata
                                    .SerializedPath);

                    refreshed.arraySize++;

                    context.SerializedObject
                        .ApplyModifiedProperties();

                    RefreshCollection(
                        context,
                        content);
                })
                {
                    text = "Add"
                };

            content.Add(addButton);
        }
    }
}