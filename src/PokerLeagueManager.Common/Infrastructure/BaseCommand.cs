using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Infrastructure
{
    [DataContract]
    public class BaseCommand : ICommand
    {
        public BaseCommand()
        {
            CommandId = Guid.NewGuid();
        }

        [DataMember]
        public Guid CommandId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string IPAddress { get; set; }
    }
}
