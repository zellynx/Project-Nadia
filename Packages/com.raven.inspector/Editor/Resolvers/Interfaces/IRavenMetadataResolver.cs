using System.Reflection;
using Metadata.Models;

namespace Editor.Resolvers.Interfaces
{
    public interface IRavenMetadataResolver
    {
        void Resolve(
            FieldInfo field,
            RavenPropertyMetadata metadata);
    }
}