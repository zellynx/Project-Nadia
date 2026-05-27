using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;

namespace Editor.Drawers.BuiltIn.Layout
{
    [CustomRavenDrawer(
        typeof(TabMetadata))]
    public sealed class
        TabDrawer
        : IRavenDrawer
    {
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Layout;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Layout;

        public int Priority => 200;

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
            foreach (var metadata in context.Node.Metadata.Metadata)
            {
                if (metadata is not TabMetadata tab)
                {
                    continue;
                }

                var container =
                    context.Tabs
                        .GetOrCreateTab(
                            tab.GroupName,
                            tab.TabName,
                            context.RenderParent);

                context.Node.Metadata
                        .RenderContainer =
                    container;
            }
        }
    }
}