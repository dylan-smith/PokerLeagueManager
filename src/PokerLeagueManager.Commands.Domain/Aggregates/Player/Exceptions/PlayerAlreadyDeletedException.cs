using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class PlayerAlreadyDeletedException : Exception
    {
        public PlayerAlreadyDeletedException(Guid playerId)
            : base(CreateMessage(playerId))
        {
        }

        protected PlayerAlreadyDeletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid playerId)
        {
            var msg = "Cannot delete a Player that has already been deleted";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Id: " + playerId.ToString();

            return msg;
        }
    }
}
