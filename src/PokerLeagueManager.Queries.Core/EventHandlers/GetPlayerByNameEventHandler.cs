using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetPlayerByNameEventHandler : BaseEventHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<GameDeletedEvent>, IHandlesEvent<PlayerRenamedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var player = QueryDataStore.GetData<GetPlayerByNameDto>().SingleOrDefault(x => x.PlayerName.ToUpper().Trim() == e.PlayerName.ToUpper().Trim());

            if (player == null)
            {
                QueryDataStore.Insert(new GetPlayerByNameDto() { PlayerName = e.PlayerName, GameCount = 1 });
            }
            else
            {
                player.GameCount++;
                QueryDataStore.SaveChanges();
            }
        }

        public void Handle(GameDeletedEvent e)
        {
            var players = QueryDataStore.GetData<LookupGamePlayersDto>().Where(x => x.GameId == e.GameId).ToList();

            foreach (var p in players)
            {
                var player = QueryDataStore.GetData<GetPlayerByNameDto>().SingleOrDefault(x => x.PlayerName.ToUpper().Trim() == p.PlayerName.ToUpper().Trim());

                if (player.GameCount <= 1)
                {
                    QueryDataStore.Delete(player);
                }
                else
                {
                    player.GameCount--;
                    QueryDataStore.SaveChanges();
                }
            }
        }

        public void Handle(PlayerRenamedEvent e)
        {
            var oldPlayer = QueryDataStore.GetData<GetPlayerByNameDto>().SingleOrDefault(x => x.PlayerName.ToUpper().Trim() == e.OldPlayerName.ToUpper().Trim());
            var newPlayer = QueryDataStore.GetData<GetPlayerByNameDto>().SingleOrDefault(x => x.PlayerName.ToUpper().Trim() == e.NewPlayerName.ToUpper().Trim());

            if (newPlayer == null)
            {
                newPlayer = new GetPlayerByNameDto() { PlayerName = e.NewPlayerName, GameCount = 0 };
                QueryDataStore.Insert(newPlayer);
            }

            oldPlayer.GameCount--;
            newPlayer.GameCount++;

            QueryDataStore.SaveChanges();

            if (oldPlayer.GameCount == 0)
            {
                QueryDataStore.Delete(oldPlayer);
            }
        }
    }
}
