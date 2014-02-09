using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Utilities.Exceptions
{
    [Serializable]
    public class GameWithNotEnoughPlayersException : Exception
    {
        public GameWithNotEnoughPlayersException()
            : base()
        {
        }

        public GameWithNotEnoughPlayersException(string message)
            : base(message)
        {
        }

        public GameWithNotEnoughPlayersException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected GameWithNotEnoughPlayersException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
