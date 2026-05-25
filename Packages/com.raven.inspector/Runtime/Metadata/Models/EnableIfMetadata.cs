using Metadata.Interfaces;

namespace Metadata.Models
{
    public sealed class EnableIfMetadata
        : IRavenMetadata
    {
        public string ConditionMember;
    }
}