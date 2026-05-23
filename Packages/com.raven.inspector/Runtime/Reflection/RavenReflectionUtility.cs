using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reflection
{
    public static class RavenReflectionUtility
    {
        private const BindingFlags Flags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        public static IEnumerable<FieldInfo> GetSerializableFields(Type type)
        {
            while (type != null)
            {
                var fields = type.GetFields(Flags);

                foreach (var field in fields)
                {
                    if (field.IsPrivate &&
                        field.GetCustomAttribute<UnityEngine.SerializeField>() == null)
                    {
                        continue;
                    }

                    if (field.IsNotSerialized)
                    {
                        continue;
                    }

                    yield return field;
                }

                type = type.BaseType;
            }
        }
    }
}