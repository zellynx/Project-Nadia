using Metadata.Interfaces;

namespace Metadata.Models
{
    public sealed class ShowIfMetadata : IRavenMetadata
    {
        public string ConditionMember;
    }
}