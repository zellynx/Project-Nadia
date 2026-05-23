using System;
using System.Collections.Generic;
using Metadata.Interfaces;

namespace Metadata.Models
{
    public sealed class RavenPropertyMetadata
    {
        public string Name;
        public string Path;

        public Type PropertyType;

        public readonly List<IRavenMetadata> Metadata = new();
    }
}