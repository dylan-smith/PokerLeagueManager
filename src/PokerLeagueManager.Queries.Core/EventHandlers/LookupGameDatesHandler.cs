using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class LookupGameDatesHandler : BaseHandler, IHandlesEvent<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            var dto = new LookupGameDatesDto();

            dto.GameId = e.GameId;
            dto.GameDate = e.GameDate;

            QueryDataStore.Insert<LookupGameDatesDto>(dto);
        }
    }
}
