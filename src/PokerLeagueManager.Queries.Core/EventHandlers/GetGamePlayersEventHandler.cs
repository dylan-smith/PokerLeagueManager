using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGamePlayersEventHandler : BaseEventHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<PlayerRemovedFromGameEvent>, IHandlesEvent<RebuyAddedEvent>, IHandlesEvent<RebuyRemovedEvent>, IHandlesEvent<GameCompletedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var player = QueryDataStore.GetData<PlayerLookupDto>().Single(p => p.PlayerId == e.PlayerId);

            QueryDataStore.Insert(new GetGamePlayersDto()
            {
                GameId = e.GameId,
                PlayerId = e.PlayerId,
                PlayerName = player.PlayerName,
            });
        }

        public void Handle(PlayerRemovedFromGameEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamePlayersDto>().Single(p => p.GameId == e.GameId && p.PlayerId == e.PlayerId);
            QueryDataStore.Delete(dto);
        }

        public void Handle(RebuyAddedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamePlayersDto>().Single(p => p.GameId == e.GameId && p.PlayerId == e.PlayerId);

            dto.PayIn += 10;

            QueryDataStore.Update(dto);
        }

        public void Handle(RebuyRemovedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamePlayersDto>().Single(p => p.GameId == e.GameId && p.PlayerId == e.PlayerId);

            dto.PayIn -= 10;

            QueryDataStore.Update(dto);
        }

        public void Handle(GameCompletedEvent e)
        {
            foreach (var p in e.Placings)
            {
                var player = QueryDataStore.GetData<PlayerLookupDto>().Single(x => x.PlayerId == p.Key);
                var dto = QueryDataStore.GetData<GetGamePlayersDto>().Single(x => x.GameId == e.GameId && x.PlayerId == player.PlayerId);

                dto.Placing = p.Value;

                if (dto.Placing == 1)
                {
                    dto.Winnings = e.First;
                }

                if (dto.Placing == 2)
                {
                    dto.Winnings = e.Second;
                }

                if (dto.Placing == 3)
                {
                    dto.Winnings = e.Third;
                }

                QueryDataStore.Update<GetGamePlayersDto>(dto);
            }
        }
    }
}
