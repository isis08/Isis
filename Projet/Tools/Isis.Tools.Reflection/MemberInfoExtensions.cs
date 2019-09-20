using System;
using System.Reflection;

namespace Isis.Tools.Reflection
{
    public static class MemberInfoExtensions
    {
        public static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            return property?.SetMethod != null;
        }
    }
}
