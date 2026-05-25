using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Layout
{
    [CustomRavenDrawer(
        typeof(TabMetadata))]
    public sealed class TabDrawer
        : IRavenDrawer
    {
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Layout;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Layout;

        public int Priority => 300;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata is TabMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is not TabMetadata tab)
                {
                    continue;
                }

                context.Tabs.Initialize(
                    context.Root);

                var container =
                    context.Tabs.GetOrCreateTab(
                        tab.Name);

                context.Node.Metadata.RenderContainer =
                    container;
            }
        }
    }
}