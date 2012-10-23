using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGameCountByDateHandler : BaseHandler, IHandlesEvent<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GetGameCountByDateDto()
            {
                GameId = e.AggregateId,
                GameYear = e.GameDate.Year,
                GameMonth = e.GameDate.Month,
                GameDay = e.GameDate.Day
            });
        }
    }
}
