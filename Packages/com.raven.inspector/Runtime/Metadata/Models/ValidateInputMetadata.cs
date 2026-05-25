using Metadata.Interfaces;

namespace Metadata.Models
{
    public sealed class ValidateInputMetadata
        : IRavenMetadata
    {
        public string ValidatorMethod;
    }
}