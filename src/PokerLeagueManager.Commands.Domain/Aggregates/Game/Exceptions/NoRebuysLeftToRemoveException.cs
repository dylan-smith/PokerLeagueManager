using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class NoRebuysLeftToRemoveException : Exception
    {
        public NoRebuysLeftToRemoveException(Guid gameId, Guid playerId)
            : base(CreateMessage(gameId, playerId))
        {
        }

        protected NoRebuysLeftToRemoveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid gameId, Guid playerId)
        {
            var msg = "This Player has no rebuys left to remove.";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Id: " + playerId.ToString() + Environment.NewLine;
            msg += "Game Id: " + gameId.ToString();

            return msg;
        }
    }
}
