using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure.Exceptions
{
    [Serializable]
    public class PublishEventFailedException : Exception
    {
        public PublishEventFailedException(IAggregateRoot aggRoot, ICommand c, Exception ex)
            : base(CreateMessage(aggRoot, c), ex)
        {
        }

        protected PublishEventFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(IAggregateRoot aggRoot, ICommand c)
        {
            var msg = "The action you were trying to perform succeeded, however not all systems have been updated yet.";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Aggregate ID: " + aggRoot.AggregateId.ToString();
            msg += Environment.NewLine;
            msg += "Command ID: " + c.CommandId.ToString();

            return msg;
        }
    }
}
