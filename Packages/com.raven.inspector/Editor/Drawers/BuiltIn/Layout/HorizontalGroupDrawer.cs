using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;

namespace Editor.Drawers.BuiltIn.Layout
{
    [CustomRavenDrawer(
        typeof(HorizontalGroupMetadata))]
    public sealed class
        HorizontalGroupDrawer
        : IRavenDrawer
    {
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Layout;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Layout;

        public int Priority => 250;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is HorizontalGroupMetadata)
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
                    is not HorizontalGroupMetadata
                    horizontal)
                {
                    continue;
                }

                var container =
                    context.HorizontalGroups
                        .GetOrCreate(
                            horizontal.Name,
                            context.RenderParent);

                context.Node.Metadata
                        .RenderContainer =
                    container;
            }
        }
    }
}