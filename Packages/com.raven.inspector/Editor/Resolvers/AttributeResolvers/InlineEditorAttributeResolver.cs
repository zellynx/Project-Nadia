using System.Reflection;
using Attributes;
using Editor.Resolvers.Interfaces;
using Metadata.Models;

namespace Editor.Resolvers.AttributeResolvers
{
    public sealed class
        InlineEditorAttributeResolver
        : IRavenMetadataResolver
    {
        public void Resolve(
            FieldInfo field,
            RavenPropertyMetadata metadata)
        {
            if (field == null)
            {
                return;
            }

            if (!field.IsDefined(
                    typeof(
                        InlineEditorAttribute),
                    true))
            {
                return;
            }

            metadata.Metadata.Add(
                new InlineEditorMetadata());
        }
    }
}