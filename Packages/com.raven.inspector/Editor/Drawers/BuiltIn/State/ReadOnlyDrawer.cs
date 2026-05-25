using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;

namespace Editor.Drawers.BuiltIn.State
{
    [CustomRavenDrawer(
        typeof(ReadOnlyMetadata))]
    public sealed class ReadOnlyDrawer : IRavenDrawer
    {
        public int Priority => 0;

        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.State;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.State;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata is ReadOnlyMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(
            RavenDrawerContext context)
        {
            if (context.CurrentElement == null)
            {
                return;
            }

            context.CurrentElement
                .SetEnabled(false);
        }
    }
}