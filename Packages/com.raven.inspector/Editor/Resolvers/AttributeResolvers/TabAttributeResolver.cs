using System.Reflection;
using Attributes;
using Editor.Resolvers.Interfaces;
using Metadata.Models;

namespace Editor.Resolvers.AttributeResolvers
{
    public sealed class TabAttributeResolver
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
                    TabAttribute>();

            if (attribute == null)
            {
                return;
            }

            metadata.Metadata.Add(
                new TabMetadata
                {
                    Name =
                        attribute.Name
                });
        }
    }
}