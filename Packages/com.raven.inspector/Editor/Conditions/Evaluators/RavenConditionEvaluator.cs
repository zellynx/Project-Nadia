using System.Reflection;
using Metadata.Models;

namespace Editor.Conditions.Evaluators
{
    public static class RavenConditionEvaluator
    {
        public static bool Evaluate(
            object target,
            ShowIfMetadata metadata)
        {
            var type = target.GetType();

            var field =
                type.GetField(
                    metadata.ConditionMember,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

            if (field == null)
            {
                return true;
            }

            if (field.FieldType != typeof(bool))
            {
                return true;
            }

            return (bool)field.GetValue(target);
        }
    }
}