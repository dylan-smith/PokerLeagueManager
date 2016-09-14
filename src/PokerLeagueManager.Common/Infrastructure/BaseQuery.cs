using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public string User { get; set; }

        [DataMember]
        public string IPAddress { get; set; }
    }
}
