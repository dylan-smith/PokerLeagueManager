using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class GameAlreadyDeletedException : Exception
    {
        public GameAlreadyDeletedException(Guid gameId)
            : base(CreateMessage(gameId))
        {
        }

        protected GameAlreadyDeletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid gameId)
        {
            var msg = "Cannot delete a Game that has already been deleted";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Game Id: " + gameId.ToString();

            return msg;
        }
    }
}
