using Editor.Conditions.Evaluators;
using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.State
{
    [CustomRavenDrawer(typeof(ShowIfMetadata))]
    public sealed class ShowIfDrawer : IRavenDrawer
    {
        public int Priority => 0;
         
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.State;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.State;

        public bool CanDraw(RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata is ShowIfMetadata)
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw(RavenDrawerContext context)
        {
            if (context.CurrentElement == null)
            {
                return;
            }

            void RefreshVisibility()
            {
                bool visible = true;

                foreach (var metadata
                         in context.Node.Metadata.Metadata)
                {
                    if (metadata is not ShowIfMetadata showIf)
                    {
                        continue;
                    }

                    visible &=
                        RavenConditionEvaluator.Evaluate(
                            context.Target,
                            showIf);
                }

                context.CurrentElement.style.display =
                    visible
                        ? DisplayStyle.Flex
                        : DisplayStyle.None;
            }

            RefreshVisibility();
            
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is not ShowIfMetadata showIf)
                {
                    continue;
                }

                var subscription =
                    context.Dependencies.Register(
                        showIf.ConditionMember,
                        RefreshVisibility);

                context.CurrentElement.RegisterCallback<
                    DetachFromPanelEvent>(
                    _ =>
                    {
                        context.Dependencies
                            .Unregister(
                                subscription);
                    });
            }
        }
    }
}