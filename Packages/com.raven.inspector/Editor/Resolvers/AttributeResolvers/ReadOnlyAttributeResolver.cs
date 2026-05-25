using System.Reflection;
using Editor.Resolvers.Interfaces;
using Metadata.Models;
using Attributes;

namespace Editor.Resolvers.AttributeResolvers
{
    public sealed class ReadOnlyAttributeResolver
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
            
            var attribute =
                field.GetCustomAttribute<
                    ReadOnlyAttribute>();

            if (attribute == null)
            {
                return;
            }

            metadata.Metadata.Add(
                new ReadOnlyMetadata());
        }
    }
}