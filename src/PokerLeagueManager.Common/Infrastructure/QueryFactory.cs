using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class QueryFactory : IQueryFactory
    {
        private OperationContext _currentContext;
        private IGuidService _guidService;
        private IDateTimeService _dateTimeService;

        public QueryFactory(OperationContext currentContext, IGuidService guidService, IDateTimeService dateTimeService)
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
            if (_currentContext != null &&
                _currentContext.ClaimsPrincipal != null &&
                _currentContext.ClaimsPrincipal.Identity != null &&
                !string.IsNullOrWhiteSpace(_currentContext.ClaimsPrincipal.Identity.Name))
            {
                query.User = _currentContext.ClaimsPrincipal.Identity.Name;
            }
            else
            {
                query.User = "Unknown";
            }

            MessageProperties prop = _currentContext.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            query.IPAddress = endpoint.Address;

            if (query.QueryId == Guid.Empty)
            {
                query.QueryId = _guidService.NewGuid();
            }

            query.Timestamp = _dateTimeService.Now();

            return query;
        }
    }
}
