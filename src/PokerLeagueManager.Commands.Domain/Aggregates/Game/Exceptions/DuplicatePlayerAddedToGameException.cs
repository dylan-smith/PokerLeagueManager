using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class DuplicatePlayerAddedToGameException : Exception
    {
        public DuplicatePlayerAddedToGameException(Guid playerId, Guid gameId)
            : base(CreateMessage(playerId, gameId))
        {
        }

        protected DuplicatePlayerAddedToGameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid playerId, Guid gameId)
        {
            var msg = "This Player has already been added to this game. Cannot add the same Player twice.";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Id: " + playerId.ToString() + Environment.NewLine;
            msg += "Game Id: " + gameId.ToString();

            return msg;
        }
    }
}
