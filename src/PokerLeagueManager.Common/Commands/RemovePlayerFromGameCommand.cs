using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class RemovePlayerFromGameCommand : BaseCommand
    {
        [DataMember]
        public Guid PlayerId { get; set; }

        [DataMember]
        public Guid GameId { get; set; }
    }
}
