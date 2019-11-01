using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class PlayerLookupEventHandler : BaseEventHandler, IHandlesEvent<PlayerCreatedEvent>
    {
        public void Handle(PlayerCreatedEvent e)
        {
            QueryDataStore.Insert(new PlayerLookupDto()
            {
                PlayerId = e.PlayerId,
                PlayerName = e.PlayerName,
            });
        }
    }
}
