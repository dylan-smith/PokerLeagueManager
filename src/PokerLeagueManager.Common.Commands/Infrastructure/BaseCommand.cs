using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Commands.Infrastructure
{
    [DataContract]
    public class BaseCommand : ICommand
    {
        

        [DataMember]
        public Guid CommandId { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public bool IsAsynchronous { get; set; }
    }
}
