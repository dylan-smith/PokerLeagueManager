using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class InvalidPlayerNameException : Exception
    {
        public InvalidPlayerNameException(string playerName)
            : base(CreateMessage(playerName))
        {
        }

        protected InvalidPlayerNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(string playerName)
        {
            var msg = "Invalid Player Name";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Name: " + playerName;

            return msg;
        }
    }
}
