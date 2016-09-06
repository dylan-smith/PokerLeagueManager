using System;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class GuidService : IGuidService
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
