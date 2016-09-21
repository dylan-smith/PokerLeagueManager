using System.Linq;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class LookupGamePlayersEventHandler : BaseEventHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<PlayerRenamedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var dto = new LookupGamePlayersDto();

            dto.GameId = e.GameId;
            dto.Winnings = e.Winnings;
            dto.PayIn = e.PayIn;
            dto.PlayerName = e.PlayerName;

            QueryDataStore.Insert<LookupGamePlayersDto>(dto);
        }

        public void Handle(PlayerRenamedEvent e)
        {
            var player = QueryDataStore.GetData<LookupGamePlayersDto>().Single(x => x.PlayerName == e.OldPlayerName && x.GameId == e.GameId);

            player.PlayerName = e.NewPlayerName;

            QueryDataStore.SaveChanges();
        }
    }
}
