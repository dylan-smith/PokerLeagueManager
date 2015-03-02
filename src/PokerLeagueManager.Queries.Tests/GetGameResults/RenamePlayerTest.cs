﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests
{
    [TestClass]
    public class RenamePlayerTest : BaseQueryTest
    {
        private Guid _gameId = Guid.NewGuid();
        private DateTime _gameDate = DateTime.Parse("17-Feb-2014");

        private string _player1 = "Dylan";
        private string _player2 = "Ryan";

        private string _newPlayerName = "Sheldon Cooper";

        private int _winnings1 = 123;
        private int _winnings2 = 10;

        private int _payin1 = 100;
        private int _payin2 = 33;

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = _gameId, GameDate = _gameDate };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId, PlayerName = _player1, Placing = 1, Winnings = _winnings1, PayIn = _payin1 };
            yield return new PlayerAddedToGameEvent { AggregateId = _gameId, PlayerName = _player2, Placing = 2, Winnings = _winnings2, PayIn = _payin2 };

            yield return new PlayerRenamedEvent() { AggregateId = _gameId, OldPlayerName = _player2, NewPlayerName = _newPlayerName };
        }

        [TestMethod]
        public void GetGameResults_RenamePlayer()
        {
            RunTest(x => x.GetGameResults(_gameId));
        }

        public override IDataTransferObject ExpectedDto()
        {
            var expectedDto = new GetGameResultsDto() { DtoId = AnyGuid(), GameId = _gameId, GameDate = _gameDate };
            expectedDto.Players.Add(new GetGameResultsDto.PlayerDto() { DtoId = AnyGuid(), PlayerName = _player1, Placing = 1, Winnings = _winnings1, PayIn = _payin1 });
            expectedDto.Players.Add(new GetGameResultsDto.PlayerDto() { DtoId = AnyGuid(), PlayerName = _newPlayerName, Placing = 2, Winnings = _winnings2, PayIn = _payin2 });

            return expectedDto;
        }
    }
}
