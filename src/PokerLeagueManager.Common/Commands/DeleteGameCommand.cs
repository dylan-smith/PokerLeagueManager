using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class DeleteGameCommand : BaseCommand
    {
        [DataMember]
        public Guid GameId { get; set; }
    }
}
