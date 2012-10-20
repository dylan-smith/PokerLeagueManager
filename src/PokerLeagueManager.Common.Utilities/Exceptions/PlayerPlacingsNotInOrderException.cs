using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Utilities.Exceptions
{
    [Serializable]
    public class PlayerPlacingsNotInOrderException : Exception
    {
        public PlayerPlacingsNotInOrderException()
            : base()
        {
        }

        public PlayerPlacingsNotInOrderException(string message)
            : base(message)
        {
        }

        public PlayerPlacingsNotInOrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PlayerPlacingsNotInOrderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
