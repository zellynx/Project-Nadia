using System;

namespace Attributes
{
    [AttributeUsage(
        AttributeTargets.Field)]
    public sealed class
        TabGroupAttribute
        : Attribute
    {
        public readonly string
            GroupName;

        public readonly string
            TabName;

        public TabGroupAttribute(
            string groupName,
            string tabName)
        {
            GroupName = groupName;
            TabName = tabName;
        }
    }
}