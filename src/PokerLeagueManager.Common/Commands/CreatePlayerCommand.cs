using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class CreatePlayerCommand : BaseCommand
    {
        [DataMember]
        public Guid PlayerId { get; set; }

        [DataMember]
        public string PlayerName { get; set; }
    }
}
