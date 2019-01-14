using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Common.Infrastructure
{
    [DataContract]
    public class BaseQuery
    {
        public BaseQuery()
        {
            QueryId = Guid.NewGuid();
        }

        [DataMember]
        public Guid QueryId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string IPAddress { get; set; }
    }
}
