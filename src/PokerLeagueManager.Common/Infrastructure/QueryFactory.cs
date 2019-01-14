using System;
using System.Web;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class QueryFactory : IQueryFactory
    {
        private readonly HttpContextWrapper _currentContext;
        private readonly IGuidService _guidService;
        private readonly IDateTimeService _dateTimeService;

        public QueryFactory(HttpContextWrapper currentContext, IGuidService guidService, IDateTimeService dateTimeService)
        {
            _currentContext = currentContext;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
        }

        public T Create<T>()
            where T : IQuery, new()
        {
            var result = new T();

            return Create(result);
        }

        public T Create<T>(T query)
            where T : IQuery
        {
            query.IPAddress = _currentContext.Request.UserHostAddress;

            if (query.QueryId == Guid.Empty)
            {
                query.QueryId = _guidService.NewGuid();
            }

            query.Timestamp = _dateTimeService.Now();

            return query;
        }
    }
}
