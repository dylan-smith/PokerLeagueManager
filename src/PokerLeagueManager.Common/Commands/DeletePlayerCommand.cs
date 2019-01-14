using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class DeletePlayerCommand : BaseCommand
    {
        [DataMember]
        public Guid PlayerId { get; set; }
    }
}
