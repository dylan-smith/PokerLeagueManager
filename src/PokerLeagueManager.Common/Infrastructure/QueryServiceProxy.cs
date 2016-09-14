using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public IDataTransferObject ExecuteQueryDto(IQuery query)
        {
            return base.Channel.ExecuteQueryDto(query);
        }

        public int ExecuteQueryInt(IQuery query)
        {
            return base.Channel.ExecuteQueryInt(query);
        }

        public IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query)
        {
            return base.Channel.ExecuteQueryList(query);
        }
    }
}
