﻿using PokerLeagueManager.Common;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class BaseCommandHandler
    {
        public IEventRepository Repository { get; set; }

        public IQueryService QueryService { get; set; }
    }
}
