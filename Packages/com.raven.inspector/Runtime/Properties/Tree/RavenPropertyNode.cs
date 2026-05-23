using System.Collections.Generic;
using Metadata.Models;

namespace Properties.Tree
{
    public sealed class RavenPropertyNode
    {
        public RavenPropertyMetadata Metadata;

        public RavenPropertyNode Parent;

        public readonly List<RavenPropertyNode> Children = new();

        public bool IsExpanded = true;
    }
}