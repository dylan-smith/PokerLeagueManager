using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PokerLeagueManager.Common.Infrastructure
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> FindHandlers<TCommand>(this Type handlerGenericType, Assembly assemblyToSearch)
        {
            if (assemblyToSearch == null)
            {
                throw new ArgumentNullException("assemblyToSearch");
            }

            return from t in assemblyToSearch.GetExportedTypes()
                   where t.IsClass &&
                         t.GetInterfaces().Where(i => i.IsGenericType &&
                                                 i.GetGenericTypeDefinition() == handlerGenericType &&
                                                 i.GetGenericArguments()[0] == typeof(TCommand)).Count() > 0
                   select t;
        }

        public static MethodInfo[] GetExtensionMethods(this Type t, Assembly extensionAssembly)
        {
            List<Type> assemblyTypes = new List<Type>();

            assemblyTypes.AddRange(extensionAssembly.GetTypes());

            var query = from type in assemblyTypes
                        where type.IsSealed && !type.IsGenericType && !type.IsNested
                        from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.GetParameters()[0].ParameterType == t
                        select method;
            return query.ToArray<MethodInfo>();
        }

        public static MethodInfo GetExtensionMethod(this Type t, Assembly extensionAssembly, string methodName)
        {
            var mi = t.GetExtensionMethods(extensionAssembly).Where(m => m.Name == methodName);

            if (mi.Count<MethodInfo>() <= 0)
            {
                return null;
            }
            else
            {
                return mi.First<MethodInfo>();
            }
        }

        public static IDictionary<string, string> GetPropertiesDictionary(this object target)
        {
            var result = new Dictionary<string, string>();

            foreach (var prop in target.GetType().GetProperties())
            {
                result.Add(prop.Name, prop.GetValue(target)?.ToString() ?? string.Empty);
            }

            return result;
        }
    }
}
