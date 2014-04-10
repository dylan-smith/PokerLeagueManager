using System;
using System.Runtime.Serialization;

namespace WCFExceptionHandling.Common
{
    [Serializable]
    public class GameWithNotEnoughPlayersException : Exception
    {
        public GameWithNotEnoughPlayersException()
            : base("Each game must have at least 2 players")
        {
        }
    }
}
