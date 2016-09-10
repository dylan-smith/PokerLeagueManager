using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Queries
{
    [DataContract]
    public class GetGameCountByDateQuery : BaseQuery
    {
        [DataMember]
        public DateTime GameDate { get; set; }
    }
}
