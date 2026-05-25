using System;
using System.Collections;
using System.Collections.Generic;

namespace Reflection
{
    public static class RavenTypeUtility
    {
        public static bool IsPrimitiveLike(Type type)
        {
            return
                type.IsPrimitive ||
                type == typeof(string) ||
                type == typeof(decimal);
        }
        
        public static bool IsCollection(Type type)
        {
            if (type == typeof(string))
            {
                return false;
            }

            return typeof(IList)
                .IsAssignableFrom(type);
        }
    }
}