using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGameCountByDateHandler : BaseEventHandler, IHandlesEvent<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GetGameCountByDateDTO()
            {
                GameId = e.AggregateId,
                GameYear = e.GameDate.Year,
                GameMonth = e.GameDate.Month,
                GameDay = e.GameDate.Day
            });
        }
    }
}
