using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Editor.Drawers.Interfaces;

namespace Editor.Drawers.Registry
{
    public static class
        RavenDrawerRegistry
    {
        public static readonly
            List<IRavenDrawer>
            Drawers;

        static RavenDrawerRegistry()
        {
            // TODO:
            // Replace with multi-assembly discovery
            // once Raven plugin architecture matures.
            
            Drawers =
                Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(type =>
                        !type.IsAbstract &&
                        typeof(IRavenDrawer)
                            .IsAssignableFrom(type))
                    .Select(type =>
                        Activator.CreateInstance(type)
                            as IRavenDrawer)
                    .Where(drawer =>
                        drawer != null)
                    .OrderBy(drawer =>
                        drawer.Phase)
                    .ToList();
        }
    }
}