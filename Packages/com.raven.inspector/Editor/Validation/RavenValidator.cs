using System.Reflection;
using Metadata.Models;

namespace Editor.Validation
{
    public static class RavenValidator
    {
        public static RavenValidationResult
            Validate(
                object target,
                object value,
                ValidateInputMetadata metadata)
        {
            var method =
                target.GetType()
                    .GetMethod(
                        metadata.ValidatorMethod,
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.NonPublic);

            if (method == null)
            {
                return new RavenValidationResult(
                    true,
                    "");
            }

            object[] args =
            {
                value,
                null
            };

            var valid =
                (bool)method.Invoke(
                    target,
                    args);

            var message =
                args[1] as string ?? "";

            return new RavenValidationResult(
                valid,
                message);
        }
    }
}