using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ValidateInputAttribute
        : Attribute
    {
        public readonly string ValidatorMethod;

        public ValidateInputAttribute(
            string validatorMethod)
        {
            ValidatorMethod =
                validatorMethod;
        }
    }
}