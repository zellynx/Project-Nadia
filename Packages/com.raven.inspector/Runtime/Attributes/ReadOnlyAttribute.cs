using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyAttribute
        : Attribute
    {
    }
}