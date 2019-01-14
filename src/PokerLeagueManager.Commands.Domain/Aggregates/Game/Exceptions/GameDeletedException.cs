using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class GameDeletedException : Exception
    {
        public GameDeletedException(Guid gameId)
            : base(CreateMessage(gameId))
        {
        }

        protected GameDeletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(Guid gameId)
        {
            var msg = "This operation is not allowed on a Game that has been deleted";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Game Id: " + gameId.ToString();

            return msg;
        }
    }
}
