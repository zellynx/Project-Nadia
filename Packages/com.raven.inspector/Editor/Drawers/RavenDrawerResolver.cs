using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Editor.Drawers.Registry;

namespace Editor.Drawers
{
    public static class RavenDrawerResolver
    {
        public static IRavenDrawer Resolve(
            RavenDrawerContext context)
        {
            foreach (var drawer in RavenDrawerRegistry.Drawers)
            {
                if (drawer.CanDraw(context))
                {
                    return drawer;
                }
            }

            return null;
        }
    }
}