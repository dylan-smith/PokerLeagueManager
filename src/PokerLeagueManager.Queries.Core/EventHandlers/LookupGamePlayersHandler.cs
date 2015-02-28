using PokerLeagueManager.Common.DTO.DataTransferObjects.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class LookupGamePlayersHandler : BaseHandler, IHandlesEvent<PlayerAddedToGameEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var dto = new LookupGamePlayersDto();

            dto.GameId = e.AggregateId;
            dto.Winnings = e.Winnings;
            dto.PayIn = e.PayIn;
            dto.PlayerName = e.PlayerName;

            QueryDataStore.Insert<LookupGamePlayersDto>(dto);
        }
    }
}
