using Editor.Conditions.Evaluators;
using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Metadata.Models;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.State
{
    [CustomRavenDrawer(
        typeof(EnableIfMetadata))]
    public sealed class EnableIfDrawer : IRavenDrawer
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
                if (metadata is EnableIfMetadata)
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

            void RefreshEnabled()
            {
                bool enabled = true;

                foreach (var metadata
                         in context.Node.Metadata.Metadata)
                {
                    if (metadata
                        is not EnableIfMetadata enableIf)
                    {
                        continue;
                    }

                    enabled &=
                        RavenConditionEvaluator
                            .Evaluate(
                                context.Target,
                                new ShowIfMetadata
                                {
                                    ConditionMember =
                                        enableIf
                                            .ConditionMember
                                });
                }

                context.CurrentElement
                    .SetEnabled(enabled);
            }

            RefreshEnabled();

            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is not EnableIfMetadata enableIf)
                {
                    continue;
                }

                var subscription =
                    context.Dependencies.Register(
                        enableIf.ConditionMember,
                        RefreshEnabled);

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