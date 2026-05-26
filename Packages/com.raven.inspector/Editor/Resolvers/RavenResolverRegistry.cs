using System.Collections.Generic;
using Editor.Resolvers.AttributeResolvers;
using Editor.Resolvers.Interfaces;

namespace Editor.Resolvers
{
    public static class RavenResolverRegistry
    {
        public static readonly List<IRavenMetadataResolver>
            Resolvers = new()
            {
                new GroupAttributeResolver(),
                new ShowIfAttributeResolver(),
                new ReadOnlyAttributeResolver(),
                new EnableIfAttributeResolver(),
                new ValidateInputAttributeResolver(),
                new TabAttributeResolver(),
                new HorizontalGroupAttributeResolver(),
                new FoldoutGroupAttributeResolver()
            };
    }
}