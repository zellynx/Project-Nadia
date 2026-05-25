using System;

namespace Editor.Drawers
{
    [AttributeUsage(
        AttributeTargets.Class)]
    public sealed class
        CustomRavenDrawerAttribute
        : Attribute
    {
        public readonly Type TargetType;

        public CustomRavenDrawerAttribute(
            Type targetType)
        {
            TargetType = targetType;
        }
    }
}