using System.Reflection;
using Attributes;
using Editor.Resolvers.Interfaces;
using Metadata.Models;

namespace Editor.Resolvers.AttributeResolvers
{
    public sealed class
        HorizontalGroupAttributeResolver
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
                    HorizontalGroupAttribute>();

            if (attribute == null)
            {
                return;
            }

            metadata.Metadata.Add(
                new HorizontalGroupMetadata
                {
                    Name =
                        attribute.Name
                });
        }
    }
}