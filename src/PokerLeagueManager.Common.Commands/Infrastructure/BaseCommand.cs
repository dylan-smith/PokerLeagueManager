﻿using System;
using System.Runtime.Serialization;

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
        public string IPAddress { get; set; }

        [DataMember]
        public bool IsAsynchronous { get; set; }
    }
}
