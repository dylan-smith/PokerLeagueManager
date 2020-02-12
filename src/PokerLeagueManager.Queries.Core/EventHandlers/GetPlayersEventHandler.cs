using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetPlayersEventHandler : BaseEventHandler,
                                          IHandlesEvent<PlayerCreatedEvent>,
                                          IHandlesEvent<PlayerDeletedEvent>,
                                          IHandlesEvent<GameCompletedEvent>,
                                          IHandlesEvent<GameUncompletedEvent>,
                                          IHandlesEvent<GameDeletedEvent>
    {
        public void Handle(PlayerCreatedEvent e)
        {
            QueryDataStore.Insert(new GetPlayersDto()
            {
                PlayerId = e.PlayerId,
                PlayerName = e.PlayerName,
                GamesPlayed = 0,
            });
        }

        public void Handle(PlayerDeletedEvent e)
        {
            var dto = QueryDataStore.GetData<GetPlayersDto>().Single(p => p.PlayerId == e.PlayerId);
            QueryDataStore.Delete(dto);
        }

        public void Handle(GameCompletedEvent e)
        {
            foreach (var player in e.Placings)
            {
                var dto = QueryDataStore.GetData<GetPlayersDto>().Single(p => p.PlayerId == player.Key);
                dto.GamesPlayed++;
                QueryDataStore.Update(dto);
            }
        }

        public void Handle(GameUncompletedEvent e)
        {
            var gamePlayersDto = QueryDataStore.GetData<GamePlayersLookupDto>().Where(x => x.GameId == e.GameId).ToList();

            foreach (var player in gamePlayersDto)
            {
                var dto = QueryDataStore.GetData<GetPlayersDto>().Single(x => x.PlayerId == player.PlayerId);
                dto.GamesPlayed--;
                QueryDataStore.Update(dto);
            }
        }

        public void Handle(GameDeletedEvent e)
        {
            var gameDto = QueryDataStore.GetData<GameLookupDto>().Single(x => x.GameId == e.GameId);

            if (!gameDto.Completed)
            {
                return;
            }

            var gamePlayersDto = QueryDataStore.GetData<GamePlayersLookupDto>().Where(x => x.GameId == e.GameId).ToList();

            foreach (var player in gamePlayersDto)
            {
                var dto = QueryDataStore.GetData<GetPlayersDto>().Single(x => x.PlayerId == player.PlayerId);
                dto.GamesPlayed--;
                QueryDataStore.Update(dto);
            }
        }
    }
}
