using System.Reflection;
using Attributes;
using Editor.Resolvers.Interfaces;
using Metadata.Models;

namespace Editor.Resolvers.AttributeResolvers
{
    public sealed class EnableIfAttributeResolver
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
                    EnableIfAttribute>();

            if (attribute == null)
            {
                return;
            }

            metadata.Metadata.Add(
                new EnableIfMetadata
                {
                    ConditionMember =
                        attribute.ConditionMember
                });
        }
    }
}