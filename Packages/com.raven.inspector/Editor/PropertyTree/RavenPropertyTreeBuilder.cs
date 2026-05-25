using System;
using System.Collections.Generic;
using System.Reflection;
using Editor.Resolvers;
using Metadata.Models;
using Properties.Tree;
using Attributes;
using Reflection;

namespace Editor.PropertyTree
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
                    SerializedPath = "",
                    PropertyType = type
                }
            };

            BuildChildren(
                root,
                type,
                new HashSet<Type>());

            return root;
        }

        private static void BuildChildren(
            RavenPropertyNode parent,
            Type type,
            HashSet<Type> visited)
        {
            if (visited.Contains(type))
            {
                return;
            }

            var currentVisited =
                new HashSet<Type>(visited);

            currentVisited.Add(type);
            
            foreach (var field in RavenReflectionUtility.GetSerializableFields(type))
            {
                var child = new RavenPropertyNode
                {
                    Parent = parent,
                    Metadata = new RavenPropertyMetadata
                    {
                        Name = field.Name,
                        Path = $"{parent.Metadata.Path}.{field.Name}",
                        SerializedPath =
                            string.IsNullOrEmpty(
                                parent.Metadata.SerializedPath)
                                ? field.Name
                                : $"{parent.Metadata.SerializedPath}.{field.Name}",
                        PropertyType = field.FieldType,
                        FieldInfo = field
                    }
                };
                
                foreach (var resolver in RavenResolverRegistry.Resolvers)
                {
                    resolver.Resolve(field, child.Metadata);
                }

                parent.Children.Add(child);
                
                if (!RavenTypeUtility.IsPrimitiveLike(
                        field.FieldType)
                    &&
                    !RavenTypeUtility.IsCollection(
                        field.FieldType))
                {
                    BuildChildren(
                        child,
                        field.FieldType,
                        currentVisited);
                }
            }
            
            var methods =
                type.GetMethods(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var button =
                    method.GetCustomAttribute<
                        ButtonAttribute>();

                if (button == null)
                {
                    continue;
                }

                if (method.GetParameters().Length > 0)
                {
                    continue;
                }

                var child =
                    new RavenPropertyNode
                    {
                        Parent = parent,

                        Metadata =
                            new RavenPropertyMetadata
                            {
                                Name = method.Name,

                                Path =
                                    $"{parent.Metadata.Path}.{method.Name}",

                                SerializedPath = "",

                                PropertyType = typeof(void),

                                MethodInfo = method
                            }
                    };

                foreach (var resolver
                         in RavenResolverRegistry.Resolvers)
                {
                    resolver.Resolve(
                        null,
                        child.Metadata);
                }

                child.Metadata.Metadata.Add(
                    new ButtonMetadata());

                parent.Children.Add(child);
            }
        }
    }
}