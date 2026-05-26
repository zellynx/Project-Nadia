using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;

namespace Editor.Drawers.BuiltIn.Layout
{
    [CustomRavenDrawer(
        typeof(FoldoutGroupMetadata))]
    public sealed class
        FoldoutGroupDrawer
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
                if (metadata
                    is FoldoutGroupMetadata)
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
                    is not FoldoutGroupMetadata
                    foldoutMetadata)
                {
                    continue;
                }

                var foldout =
                    context.Foldouts
                        .GetOrCreate(
                            foldoutMetadata.Name,
                            context.RenderParent);

                context.Node.Metadata
                        .RenderContainer =
                    foldout.contentContainer;
            }
        }
    }
}