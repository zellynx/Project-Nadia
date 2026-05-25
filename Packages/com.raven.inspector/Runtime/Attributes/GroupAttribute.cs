using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class GroupAttribute : Attribute
    {
        public readonly string Name;

        public GroupAttribute(string name)
        {
            Name = name;
        }
    }
}