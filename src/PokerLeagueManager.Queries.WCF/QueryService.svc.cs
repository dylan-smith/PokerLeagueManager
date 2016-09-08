using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Queries.WCF.Infrastructure;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        private IQueryService _queryHandler;

        public QueryService()
        {
            _queryHandler = Resolver.Container.Resolve<IQueryService>();
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            return _queryHandler.GetGameCountByDate(gameDate);
        }

        public IEnumerable<GetGamesListDto> GetGamesList()
        {
            return _queryHandler.GetGamesList();
        }

        public IEnumerable<GetGamePlayersDto> GetGamePlayers(Guid gameId)
        {
            return _queryHandler.GetGamePlayers(gameId);
        }

        public IEnumerable<GetPlayerStatisticsDto> GetPlayerStatistics()
        {
            return _queryHandler.GetPlayerStatistics();
        }

        public IEnumerable<GetPlayerGamesDto> GetPlayerGames(string playerName)
        {
            return _queryHandler.GetPlayerGames(playerName);
        }

        public IEnumerable<GetGamesWithPlayerDto> GetGamesWithPlayer(string playerName)
        {
            return _queryHandler.GetGamesWithPlayer(playerName);
        }

        public GetPlayerByNameDto GetPlayerByName(string playerName)
        {
            return _queryHandler.GetPlayerByName(playerName);
        }
    }
}
