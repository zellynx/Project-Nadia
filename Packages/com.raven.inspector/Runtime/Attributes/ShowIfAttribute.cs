using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ShowIfAttribute : Attribute
    {
        public readonly string ConditionMember;

        public ShowIfAttribute(string conditionMember)
        {
            ConditionMember = conditionMember;
        }
    }
}