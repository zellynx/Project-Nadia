using System.Linq;
using Editor.Drawers.Contexts;
using Editor.Drawers.Registry;
using Editor.Layout;
using Editor.Reactive;
using Editor.UI.Elements;
using Properties.Tree;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Drawers
{
    public static class RavenDrawerPipeline
    {
        public static void Draw(
            RavenPropertyNode node,
            VisualElement root,
            SerializedObject serializedObject,
            object target)
        {
            var groups =
                new RavenGroupContainerRegistry();
            var tabs =
                new RavenTabRegistry();
            var horizontalGroups =
                new RavenHorizontalGroupRegistry();
            var foldouts =
                new RavenFoldoutRegistry();
            var dependencies =
                new RavenDependencyRegistry();
            var renderStack =
                new RavenRenderStack(root);

            DrawRecursive(
                node,
                root,
                serializedObject,
                groups,
                tabs,
                horizontalGroups,
                foldouts,
                dependencies,
                target,
                renderStack);
        }

        private static void DrawRecursive(
            RavenPropertyNode node,
            VisualElement root,
            SerializedObject serializedObject,
            RavenGroupContainerRegistry groups,
            RavenTabRegistry tabs,
            RavenHorizontalGroupRegistry
                horizontalGroups,
            RavenFoldoutRegistry
                foldouts,
            RavenDependencyRegistry dependencies,
            object target,
            RavenRenderStack
                renderStack)
        {

            var context = new RavenDrawerContext
            {
                Node = node,
                Root = root,
                RenderParent = renderStack.Current,
                SerializedObject = serializedObject,
                Groups = groups,
                Tabs = tabs,
                HorizontalGroups = horizontalGroups,
                Foldouts = foldouts,
                Dependencies = dependencies,
                Target = target,
                RenderStack = renderStack
            };

            var orderedDrawers =
                RavenDrawerRegistry.Drawers;
            
            foreach (var drawer in orderedDrawers)
            {
                if (drawer.Layer !=
                    RavenDrawerLayer.Layout)
                {
                    continue;
                }

                if (!drawer.CanDraw(context))
                {
                    continue;
                }

                drawer.Draw(context);
            }
            
            context.RenderParent =
                node.Metadata.RenderContainer
                ??
                context.RenderParent;

            var structuralDrawers =
                orderedDrawers
                    .Where(x =>
                        x.Layer ==
                        RavenDrawerLayer.Structural)
                    .Where(x =>
                        x.CanDraw(context))
                    .OrderByDescending(x =>
                        x.Priority)
                    .ToList();

            if (structuralDrawers.Count > 0)
            {
                structuralDrawers[0]
                    .Draw(context);
            }

            foreach (var drawer in orderedDrawers)
            {
                if (drawer.Layer ==
                    RavenDrawerLayer.Layout
                    ||
                    drawer.Layer ==
                    RavenDrawerLayer.Structural)
                {
                    continue;
                }

                if (!drawer.CanDraw(context))
                {
                    continue;
                }

                drawer.Draw(context);
            }

            VisualElement previousParent =
                renderStack.Current;

            VisualElement childParent =
                node.Metadata.RenderContainer
                ??
                previousParent;

            renderStack.Push(childParent);

            foreach (var child in node.Children)
            {
                DrawRecursive(
                    child,
                    root,
                    serializedObject,
                    groups,
                    tabs,
                    horizontalGroups,
                    foldouts,
                    dependencies,
                    target,
                    renderStack
                    );
            }

            renderStack.Pop();
        }
    }
}