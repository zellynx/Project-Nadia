using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Actions
{
    [CustomRavenDrawer(typeof(ButtonMetadata))]
    public sealed class ButtonDrawer : IRavenDrawer
    {
        public int Priority => 100;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Value;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Structural;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata is ButtonMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(
            RavenDrawerContext context)
        {
            var method =
                context.Node.Metadata.MethodInfo;

            if (method == null)
            {
                return;
            }

            var button =
                new Button(() =>
                {
                    method.Invoke(
                        context.Target,
                        null);
                })
                {
                    text =
                        ObjectNames
                            .NicifyVariableName(
                                method.Name)
                };
            
            button.AddToClassList(
                "raven-button");

            context.RenderParent.Add(button);

            context.CurrentElement =
                button;
        }
    }
}