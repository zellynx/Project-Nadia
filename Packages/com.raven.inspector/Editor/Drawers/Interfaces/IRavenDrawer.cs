using Editor.Drawers.Contexts;

namespace Editor.Drawers.Interfaces
{
    public interface IRavenDrawer
    {
        int Priority { get; }
        
        RavenDrawerPhase Phase { get; }

        RavenDrawerLayer Layer { get; }

        bool CanDraw(RavenDrawerContext context);

        void Draw(RavenDrawerContext context);
    }
}