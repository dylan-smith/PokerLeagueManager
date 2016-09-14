using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.QueryHandlers
{
    public class GetGameCountByDateQueryHandler : BaseQueryHandler, IHandlesQuery<GetGameCountByDateQuery, int>
    {
        public int Execute(GetGameCountByDateQuery query)
        {
            return Repository.GetData<GetGameCountByDateDto>().Count(x => x.GameYear == query.GameDate.Year &&
                                                                          x.GameMonth == query.GameDate.Month &&
                                                                          x.GameDay == query.GameDate.Day);
        }
    }
}