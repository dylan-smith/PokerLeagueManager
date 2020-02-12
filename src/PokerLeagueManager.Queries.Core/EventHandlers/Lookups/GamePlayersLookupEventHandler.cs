using System.Linq;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GamePlayersLookupEventHandler : BaseEventHandler,
                                                 IHandlesEvent<PlayerAddedToGameEvent>,
                                                 IHandlesEvent<PlayerRemovedFromGameEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            QueryDataStore.Insert(new GamePlayersLookupDto()
            {
                GameId = e.GameId,
                PlayerId = e.PlayerId,
            });
        }

        public void Handle(PlayerRemovedFromGameEvent e)
        {
            var dto = QueryDataStore.GetData<GamePlayersLookupDto>().Single(x => x.GameId == e.GameId && x.PlayerId == e.PlayerId);
            QueryDataStore.Delete(dto);
        }
    }
}
