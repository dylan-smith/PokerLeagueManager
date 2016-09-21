using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetPlayerStatisticsEventHandler : BaseEventHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<GameDeletedEvent>, IHandlesEvent<PlayerRenamedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var player = QueryDataStore.GetData<GetPlayerStatisticsDto>().FirstOrDefault(x => x.PlayerName.ToUpper() == e.PlayerName.ToUpper());

            if (player == null)
            {
                player = new GetPlayerStatisticsDto();

                AddGameToPlayer(player, e);

                QueryDataStore.Insert(player);
            }
            else
            {
                AddGameToPlayer(player, e);

                QueryDataStore.SaveChanges();
            }
        }

        public void Handle(GameDeletedEvent e)
        {
            var players = QueryDataStore.GetData<LookupGamePlayersDto>().Where(x => x.GameId == e.GameId).ToList();

            foreach (var p in players)
            {
                var stats = QueryDataStore.GetData<GetPlayerStatisticsDto>().Single(x => x.PlayerName.ToUpper() == p.PlayerName.ToUpper());

                stats.GamesPlayed--;
                stats.Winnings -= p.Winnings;
                stats.PayIn -= p.PayIn;
                stats.Profit -= p.Winnings - p.PayIn;
                stats.ProfitPerGame = stats.GamesPlayed == 0 ? 0 : (double)stats.Profit / stats.GamesPlayed;

                if (stats.GamesPlayed == 0)
                {
                    QueryDataStore.Delete(stats);
                }
            }

            QueryDataStore.SaveChanges();
        }

        public void Handle(PlayerRenamedEvent e)
        {
            var oldPlayer = QueryDataStore.GetData<GetPlayerStatisticsDto>().Single(x => x.PlayerName.ToUpper() == e.OldPlayerName.ToUpper());
            var stats = QueryDataStore.GetData<LookupGamePlayersDto>().Single(x => x.PlayerName.ToUpper() == e.OldPlayerName.ToUpper() && x.GameId == e.GameId);

            oldPlayer.GamesPlayed--;
            oldPlayer.Winnings -= stats.Winnings;
            oldPlayer.PayIn -= stats.PayIn;
            oldPlayer.Profit -= stats.Winnings - stats.PayIn;
            oldPlayer.ProfitPerGame = oldPlayer.GamesPlayed == 0 ? 0 : (double)oldPlayer.Profit / oldPlayer.GamesPlayed;

            if (oldPlayer.GamesPlayed == 0)
            {
                QueryDataStore.Delete(oldPlayer);
            }

            var newPlayer = QueryDataStore.GetData<GetPlayerStatisticsDto>().FirstOrDefault(x => x.PlayerName.ToUpper() == e.NewPlayerName.ToUpper());

            if (newPlayer == null)
            {
                newPlayer = new GetPlayerStatisticsDto();
                newPlayer.PlayerName = e.NewPlayerName;
                QueryDataStore.Insert(newPlayer);
            }

            newPlayer.GamesPlayed++;
            newPlayer.Winnings += stats.Winnings;
            newPlayer.PayIn += stats.PayIn;
            newPlayer.Profit += stats.Winnings - stats.PayIn;
            newPlayer.ProfitPerGame = newPlayer.GamesPlayed == 0 ? 0 : (double)newPlayer.Profit / newPlayer.GamesPlayed;

            QueryDataStore.SaveChanges();
        }

        private void AddGameToPlayer(GetPlayerStatisticsDto player, PlayerAddedToGameEvent e)
        {
            if (player.PlayerName == null || player.PlayerName.ToUpper() != e.PlayerName.ToUpper())
            {
                player.PlayerName = e.PlayerName;
            }

            player.GamesPlayed++;
            player.Winnings += e.Winnings;
            player.PayIn += e.PayIn;
            player.Profit += e.Winnings - e.PayIn;
            player.ProfitPerGame = (double)player.Profit / player.GamesPlayed;
        }
    }
}
