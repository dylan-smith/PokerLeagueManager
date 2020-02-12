﻿using System;
using System.Runtime.Serialization;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Common.Commands
{
    [DataContract]
    public class ChangeGameDateCommand : BaseCommand
    {
        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public DateTime GameDate { get; set; }
    }
}
