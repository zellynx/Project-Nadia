using System;

namespace Attributes
{
    [AttributeUsage(
        AttributeTargets.Field)]
    public sealed class
        FoldoutGroupAttribute
        : Attribute
    {
        public readonly string Name;

        public FoldoutGroupAttribute(
            string name)
        {
            Name = name;
        }
    }
}