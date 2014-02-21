using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Utilities.Exceptions
{
    [Serializable]
    public class PublishEventFailedException : Exception
    {
        public PublishEventFailedException()
            : base()
        {
        }

        public PublishEventFailedException(string message)
            : base(message)
        {
        }

        public PublishEventFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PublishEventFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
