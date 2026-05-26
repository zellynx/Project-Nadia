using System;

namespace Attributes
{
    [AttributeUsage(
        AttributeTargets.Field)]
    public sealed class
        HorizontalGroupAttribute
        : Attribute
    {
        public readonly string Name;

        public HorizontalGroupAttribute(
            string name)
        {
            Name = name;
        }
    }
}