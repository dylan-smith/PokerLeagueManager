using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests.GetPlayerByName
{
    [TestClass]
    public class PlayerDeletedOneOutOfTwoGamesTest : BaseQueryTest
    {
        private Guid _gameId1 = Guid.NewGuid();
        private DateTime _gameDate1 = DateTime.Parse("17-Feb-2014");

        private Guid _gameId2 = Guid.NewGuid();
        private DateTime _gameDate2 = DateTime.Parse("17-Feb-2015");

        private string _player1 = "Dylan";
        private string _player2 = "Ryan";

        private int _winnings1 = 123;
        private int _winnings2 = 10;

        private int _payin1 = 100;
        private int _payin2 = 33;

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = _gameId1, GameDate = _gameDate1 };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId1, PlayerName = _player1, Placing = 1, Winnings = _winnings1, PayIn = _payin1 };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId1, PlayerName = _player2, Placing = 2, Winnings = _winnings2, PayIn = _payin2 };

            yield return new GameCreatedEvent() { AggregateId = _gameId2, GameDate = _gameDate2 };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId2, PlayerName = _player1, Placing = 1, Winnings = _winnings1, PayIn = _payin1 };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId2, PlayerName = _player2, Placing = 2, Winnings = _winnings2, PayIn = _payin2 };

            yield return new GameDeletedEvent() { GameId = _gameId1 };
        }

        [TestMethod]
        public void GetPlayerByName_PlayerDeletedOneOutOfTwoGames()
        {
            RunTest(x => x.GetPlayerByName(_player1));
        }

        public override IDataTransferObject ExpectedDto()
        {
            return new GetPlayerByNameDto() { PlayerName = _player1, GameCount = 1 };
        }
    }
}
