using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Group
{
    [CustomRavenDrawer(typeof(GroupMetadata))]
    public sealed class GroupDrawer : IRavenDrawer
    {
        public int Priority => 200;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Layout;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Layout;
        
        public bool CanDraw(RavenDrawerContext context)
        {
            foreach (var metadata in context.Node.Metadata.Metadata)
            {
                if (metadata is GroupMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(RavenDrawerContext context)
        {
            foreach (var metadata in context.Node.Metadata.Metadata)
            {
                if (metadata is not GroupMetadata group)
                {
                    continue;
                }

                if (context.Groups.TryGetGroup(
                        group.GroupName,
                        out _))
                {
                    return;
                }

                var foldout = new Foldout
                {
                    text = group.GroupName,
                    value = true
                };

                context.Root.Add(foldout);

                context.Groups.RegisterGroup(
                    group.GroupName,
                    foldout);
            }
        }
    }
}