using System;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class WinningsDoesNotEqualPayinsException : Exception
    {
        public WinningsDoesNotEqualPayinsException()
            : base("The total player Winnings must be equal to the total player Payins")
        {
        }
    }
}
