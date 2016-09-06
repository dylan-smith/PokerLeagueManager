using System;
using System.Collections.Generic;
using System.ServiceModel;
using PokerLeagueManager.Common.DTO;

namespace PokerLeagueManager.Common
{
    public class QueryServiceProxy : ClientBase<IQueryService>, IQueryService
    {
        public int GetGameCountByDate(DateTime gameDate)
        {
            return base.Channel.GetGameCountByDate(gameDate);
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            return base.Channel.GetGamesList();
        }

        public GetGameResultsDto GetGameResults(Guid gameId)
        {
            return base.Channel.GetGameResults(gameId);
        }

        public IEnumerable<GetPlayerStatisticsDto> GetPlayerStatistics()
        {
            return base.Channel.GetPlayerStatistics();
        }

        public IEnumerable<GetPlayerGamesDto> GetPlayerGames(string playerName)
        {
            return base.Channel.GetPlayerGames(playerName);
        }

        public IEnumerable<GetGamesWithPlayerDto> GetGamesWithPlayer(string playerName)
        {
            return base.Channel.GetGamesWithPlayer(playerName);
        }

        public GetPlayerByNameDto GetPlayerByName(string playerName)
        {
            return base.Channel.GetPlayerByName(playerName);
        }
    }
}
