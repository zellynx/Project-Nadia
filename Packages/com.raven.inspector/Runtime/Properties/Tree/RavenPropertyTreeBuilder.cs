using System;
using Metadata.Models;
using Reflection;

namespace Properties.Tree
{
    public static class RavenPropertyTreeBuilder
    {
        public static RavenPropertyNode Build(Type type)
        {
            var root = new RavenPropertyNode
            {
                Metadata = new RavenPropertyMetadata
                {
                    Name = type.Name,
                    Path = type.Name,
                    PropertyType = type
                }
            };

            BuildChildren(root, type);

            return root;
        }

        private static void BuildChildren(
            RavenPropertyNode parent,
            Type type)
        {
            foreach (var field in RavenReflectionUtility.GetSerializableFields(type))
            {
                var child = new RavenPropertyNode
                {
                    Parent = parent,
                    Metadata = new RavenPropertyMetadata
                    {
                        Name = field.Name,
                        Path = $"{parent.Metadata.Path}.{field.Name}",
                        PropertyType = field.FieldType
                    }
                };

                parent.Children.Add(child);
            }
        }
    }
}