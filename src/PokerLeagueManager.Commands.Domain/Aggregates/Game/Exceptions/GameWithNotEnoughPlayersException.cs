using System;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class GameWithNotEnoughPlayersException : Exception
    {
        public GameWithNotEnoughPlayersException()
            : base("Each game must have at least 2 players")
        {
        }
    }
}
