﻿using System;
using System.Runtime.Serialization;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class InvalidGameDateException : Exception
    {
        public InvalidGameDateException(DateTime gameDate)
            : base(CreateMessage(gameDate))
        {
        }

        protected InvalidGameDateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string CreateMessage(DateTime gameDate)
        {
            var msg = "Invalid Game Date";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Game Date: " + gameDate.ToString();

            return msg;
        }
    }
}
