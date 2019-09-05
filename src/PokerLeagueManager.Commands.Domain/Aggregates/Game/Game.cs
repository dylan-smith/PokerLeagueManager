using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Domain.Aggregates
{
    public class Game : BaseAggregateRoot
    {
        private readonly Dictionary<Guid, (int Rebuys, int Placing)> _players = new Dictionary<Guid, (int Rebuys, int Placing)>();
        private readonly LinkedList<Guid> _knockedOutOrder = new LinkedList<Guid>();
        private bool _deleted = false;
        private bool _completed = false;

        public Game(Guid gameId, DateTime gameDate)
        {
            if (gameDate == DateTime.MinValue || gameDate == DateTime.MaxValue || gameDate == default(DateTime))
            {
                throw new InvalidGameDateException(gameDate);
            }

            gameId = gameId == Guid.Empty ? Guid.NewGuid() : gameId;

            PublishEvent(new GameCreatedEvent() { GameId = gameId, GameDate = gameDate });
        }

        private Game()
        {
        }

        public void DeleteGame()
        {
            if (_deleted)
            {
                throw new GameAlreadyDeletedException(base.AggregateId);
            }

            base.PublishEvent(new GameDeletedEvent() { GameId = base.AggregateId });
        }

        public void AddPlayerToGame(Player player)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (player.IsDeleted())
            {
                throw new PlayerDeletedException(player.PlayerId);
            }

            if (_players.ContainsKey(player.PlayerId))
            {
                throw new DuplicatePlayerAddedToGameException(player.PlayerId, base.AggregateId);
            }

            base.PublishEvent(new PlayerAddedToGameEvent() { GameId = base.AggregateId, PlayerId = player.PlayerId });
            RecalculatePayouts();

            if (_completed)
            {
                base.PublishEvent(new GameUncompletedEvent() { GameId = base.AggregateId });
            }
        }

        public void RemovePlayerFromGame(Guid playerId)
        {
            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            base.PublishEvent(new PlayerRemovedFromGameEvent() { GameId = base.AggregateId, PlayerId = playerId });
            RecalculatePayouts();

            if (_completed)
            {
                base.PublishEvent(new GameUncompletedEvent() { GameId = base.AggregateId });
                CompleteGame();
            }
        }

        public void AddRebuy(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            base.PublishEvent(new RebuyAddedEvent() { GameId = base.AggregateId, PlayerId = playerId });
            RecalculatePayouts();

            if (_completed)
            {
                base.PublishEvent(new GameUncompletedEvent() { GameId = base.AggregateId });
                CompleteGame();
            }
        }

        public void RemoveRebuy(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            if (_players[playerId].Rebuys <= 0)
            {
                throw new NoRebuysLeftToRemoveException(base.AggregateId, playerId);
            }

            base.PublishEvent(new RebuyRemovedEvent() { GameId = base.AggregateId, PlayerId = playerId });
            RecalculatePayouts();

            if (_completed)
            {
                base.PublishEvent(new GameUncompletedEvent() { GameId = base.AggregateId });
                CompleteGame();
            }
        }

        public void KnockoutPlayer(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            if (_players[playerId].Placing != 0)
            {
                throw new PlayerAlreadyKnockedOutException(base.AggregateId, playerId);
            }

            base.PublishEvent(new PlayerKnockedOutEvent() { GameId = base.AggregateId, PlayerId = playerId });

            if (_knockedOutOrder.Count == _players.Count - 1)
            {
                var lastPlayer = _players.Keys.First(p => !_knockedOutOrder.Contains(p));
                base.PublishEvent(new PlayerKnockedOutEvent() { GameId = base.AggregateId, PlayerId = lastPlayer });

                CompleteGame();
            }
        }

        public void UnKnockoutPlayer(Guid playerId)
        {
            if (_deleted)
            {
                throw new GameDeletedException(base.AggregateId);
            }

            if (!_players.ContainsKey(playerId))
            {
                throw new PlayerNotInGameException(playerId, base.AggregateId);
            }

            if (_players[playerId].Placing == 0)
            {
                throw new PlayerNotKnockedOutException(base.AggregateId, playerId);
            }

            base.PublishEvent(new PlayerUnKnockedOutEvent() { GameId = base.AggregateId, PlayerId = playerId });

            if (_completed)
            {
                base.PublishEvent(new GameUncompletedEvent() { GameId = base.AggregateId });
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Plumbing needs this method signature to exist to work properly")]
        protected void ApplyEvent(GameCompletedEvent e)
        {
            _completed = true;
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Plumbing needs this method signature to exist to work properly")]
        protected void ApplyEvent(GameUncompletedEvent e)
        {
            _completed = false;
        }

        protected void ApplyEvent(PlayerUnKnockedOutEvent e)
        {
            _players[e.PlayerId] = (_players[e.PlayerId].Rebuys, 0);
            _knockedOutOrder.Remove(e.PlayerId);
        }

        protected void ApplyEvent(GameCreatedEvent e)
        {
            base.AggregateId = e.GameId;
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Plumbing needs this method signature to exist to work properly")]
        protected void ApplyEvent(GameDeletedEvent e)
        {
            _deleted = true;
        }

        protected void ApplyEvent(PlayerAddedToGameEvent e)
        {
            _players.Add(e.PlayerId, (0, 0));
        }

        protected void ApplyEvent(PlayerRemovedFromGameEvent e)
        {
            _players.Remove(e.PlayerId);
            _knockedOutOrder.Remove(e.PlayerId);
        }

        protected void ApplyEvent(RebuyAddedEvent e)
        {
            _players[e.PlayerId] = (_players[e.PlayerId].Rebuys + 1, _players[e.PlayerId].Placing);
        }

        protected void ApplyEvent(RebuyRemovedEvent e)
        {
            _players[e.PlayerId] = (_players[e.PlayerId].Rebuys - 1, _players[e.PlayerId].Placing);
        }

        protected void ApplyEvent(PlayerKnockedOutEvent e)
        {
            var placing = _players.Count - _players.Count(x => x.Value.Placing != 0);
            _players[e.PlayerId] = (_players[e.PlayerId].Rebuys, placing);
            _knockedOutOrder.AddFirst(e.PlayerId);
        }

        private void RecalculatePayouts()
        {
            var (first, second, third) = CalculatePayouts();

            base.PublishEvent(new PayoutsCalculatedEvent() { GameId = base.AggregateId, First = first, Second = second, Third = third });
        }

        private (int first, int second, int third) CalculatePayouts()
        {
            var rake = 10;
            var buyin = 20;
            var rebuy = 10;
            var secondPercent = 0.3;
            var thirdPercent = 0.1;

            var totalPot = (_players.Count * buyin) + (_players.Sum(x => x.Value.Rebuys) * rebuy) - rake;

            if (totalPot < 150)
            {
                secondPercent = 0.3;
                thirdPercent = 0;
            }

            var third = RoundToNearestTen(totalPot * thirdPercent);
            var second = RoundToNearestTen(totalPot * secondPercent);
            var first = RoundToNearestTen(totalPot - third - second);

            return (first, second, third);
        }

        private int RoundToNearestTen(double value)
        {
            return (int)(Math.Round(value / 10.0, MidpointRounding.AwayFromZero) * 10);
        }

        private void CompleteGame()
        {
            var (first, second, third) = CalculatePayouts();
            var completedEvent = new GameCompletedEvent() { GameId = base.AggregateId, First = first, Second = second, Third = third };

            foreach (var p in _players)
            {
                completedEvent.Placings.Add(p.Key, p.Value.Placing);
            }

            base.PublishEvent(completedEvent);
        }
    }
}
