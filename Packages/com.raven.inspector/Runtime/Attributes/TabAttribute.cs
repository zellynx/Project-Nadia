using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TabAttribute
        : Attribute
    {
        public readonly string Name;

        public TabAttribute(
            string name)
        {
            Name = name;
        }
    }
}