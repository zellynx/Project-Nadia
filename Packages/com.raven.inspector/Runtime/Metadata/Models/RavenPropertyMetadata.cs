using System;
using System.Collections.Generic;
using System.Reflection;
using Metadata.Interfaces;
using UnityEngine.UIElements;

namespace Metadata.Models
{
    public sealed class RavenPropertyMetadata
    {
        public string Name;
        public string Path;
        public string SerializedPath;

        public Type PropertyType;
        public string PropertyPath;

        public FieldInfo FieldInfo;
        public MethodInfo MethodInfo;
        
        public VisualElement RenderContainer;
        public VisualElement ContentContainer;

        public readonly List<IRavenMetadata> Metadata = new();
    }
}