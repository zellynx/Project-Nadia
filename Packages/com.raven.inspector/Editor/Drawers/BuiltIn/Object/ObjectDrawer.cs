using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Reflection;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Object
{
    public sealed class ObjectDrawer : IRavenDrawer
    {
        public int Priority => 10;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Layout;

        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Structural;
        
        public bool CanDraw(RavenDrawerContext context)
        {
            if (context.Node.Metadata.MethodInfo
                != null)
            {
                return false;
            }
            
            var type =
                context.Node.Metadata.PropertyType;

            if (type == null)
            {
                return false;
            }

            return
                !RavenTypeUtility.IsPrimitiveLike(type)
                &&
                !RavenTypeUtility.IsCollection(type);   
        }

        public void Draw(RavenDrawerContext context)
        {
            if (context.Node.Parent == null)
            {
                return;
            }

            var foldout = new Foldout
            {
                text = context.Node.Metadata.Name,
                value = true
            };
            
            foldout.AddToClassList(
                "raven-group");

            context.Root.Add(foldout);

            context.CurrentElement = foldout;

            context.Node.Metadata.RenderContainer =
                foldout;
        }
    }
}