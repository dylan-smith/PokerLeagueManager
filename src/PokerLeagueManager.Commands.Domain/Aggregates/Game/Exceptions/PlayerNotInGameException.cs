using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class PlayerNotInGameException : Exception
    {
        public PlayerNotInGameException(Guid playerId, Guid gameId)
            : base(CreateMessage(playerId, gameId))
        {
        }

        protected PlayerNotInGameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid playerId, Guid gameId)
        {
            var msg = "This Player is not in this game. Could not perform the operation.";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Id: " + playerId.ToString() + Environment.NewLine;
            msg += "Game Id: " + gameId.ToString();

            return msg;
        }
    }
}
