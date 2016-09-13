using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace PokerLeagueManager.Common.Infrastructure
{
    public static class QueryTypeProvider
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "provider", Justification = "Parameter required by function")]
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            var queryTypes = (from t in Assembly.GetExecutingAssembly().GetExportedTypes()
                             where t.IsClass && t.GetInterfaces().Where(i => i == typeof(IQuery)).Count() > 0
                             select t).ToList();

            var dtoTypes = DtoTypeProvider.GetKnownTypes(null).ToList();

            queryTypes.AddRange(dtoTypes);

            return queryTypes;
        }
    }
}
