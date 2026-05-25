using Editor.Drawers.Contexts;
using Editor.Drawers.Interfaces;
using Editor.Utility;
using Editor.Validation;
using Metadata.Models;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Drawers.BuiltIn.Decorator
{
    [CustomRavenDrawer(
        typeof(ValidateInputMetadata))]
    public sealed class ValidationDrawer : IRavenDrawer
    {
        public int Priority => 0;
        
        public RavenDrawerPhase Phase =>
            RavenDrawerPhase.Decorator;
        
        public RavenDrawerLayer Layer =>
            RavenDrawerLayer.Decorator;

        public bool CanDraw(
            RavenDrawerContext context)
        {
            foreach (var metadata
                     in context.Node.Metadata.Metadata)
            {
                if (metadata
                    is ValidateInputMetadata)
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
            
            var helpBox =
                new HelpBox(
                    "",
                    HelpBoxMessageType.Error);
            
            helpBox.AddToClassList(
                "raven-validation-error");

            void RefreshValidation()
            {
                bool valid = true;

                string message = "";

                foreach (var metadata
                         in context.Node.Metadata.Metadata)
                {
                    if (metadata
                        is not ValidateInputMetadata
                            validate)
                    {
                        continue;
                    }

                    var property =
                        context.SerializedObject.FindProperty(
                            context.Node.Metadata.SerializedPath);

                    if (property == null)
                    {
                        return;
                    }

                    var value =
                        RavenSerializedPropertyUtility
                            .GetValue(property);

                    var result =
                        RavenValidator.Validate(
                            context.Target,
                            value,
                            validate);

                    valid &= result.Valid;

                    if (!result.Valid)
                    {
                        message =
                            result.Message;
                    }
                }

                helpBox.style.display =
                    valid
                        ? DisplayStyle.None
                        : DisplayStyle.Flex;

                helpBox.text = message;
            }

            RefreshValidation();

            var subscription =
                context.Dependencies.Register(
                    context.Node.Metadata.Name,
                    RefreshValidation);

            context.CurrentElement.RegisterCallback<
                DetachFromPanelEvent>(
                _ =>
                {
                    context.Dependencies
                        .Unregister(
                            subscription);
                });

            context.RenderParent.Add(
                helpBox);
        }
    }
}