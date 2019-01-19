using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.PayoutsCalculated
{
    [TestClass]
    public class PayoutsCalculated60To300For2018Test : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId1 = Guid.NewGuid();
        private Guid _playerId2 = Guid.NewGuid();
        private Guid _playerId3 = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = new DateTime(2018, 7, 3) };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId1, PlayerName = "Homer Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId2, PlayerName = "Bart Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId3, PlayerName = "Rick Sanchez" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId1 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId2 };
        }

        [TestMethod]
        public void PayoutsCalculated60To300()
        {
            var commands = new List<ICommand>();
            commands.Add(new AddPlayerToGameCommand() { GameId = _gameId, PlayerId = _playerId3 });

            for (var i = 0; i < 25; i++)
            {
                commands.Add(new AddRebuyCommand() { GameId = _gameId, PlayerId = _playerId3 });
            }

            RunTest(commands);
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 30, Second = 20, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 40, Second = 20, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 50, Second = 20, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 60, Second = 20, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 60, Second = 30, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 70, Second = 30, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 80, Second = 30, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 80, Second = 40, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 90, Second = 40, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 100, Second = 40, Third = 0 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 80, Second = 50, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 90, Second = 50, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 100, Second = 50, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 110, Second = 50, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 110, Second = 60, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 120, Second = 60, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 130, Second = 60, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 130, Second = 70, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 140, Second = 70, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 150, Second = 70, Third = 20 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 140, Second = 80, Third = 30 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 150, Second = 80, Third = 30 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 160, Second = 80, Third = 30 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 170, Second = 80, Third = 30 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 170, Second = 90, Third = 30 };
            yield return new VerifyEventsNow();

            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 180, Second = 90, Third = 30 };
            yield return new VerifyEventsNow();
        }
    }
}
