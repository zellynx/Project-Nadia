using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnableIfAttribute
        : Attribute
    {
        public readonly string ConditionMember;

        public EnableIfAttribute(
            string conditionMember)
        {
            ConditionMember =
                conditionMember;
        }
    }
}