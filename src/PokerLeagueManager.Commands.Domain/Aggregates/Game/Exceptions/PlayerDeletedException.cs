using System;

namespace PokerLeagueManager.Commands.Domain.Exceptions
{
    [Serializable]
    public class PlayerDeletedException : Exception
    {
        public PlayerDeletedException(Guid playerId)
            : base(CreateMessage(playerId))
        {
        }

        private static string CreateMessage(Guid playerId)
        {
            var msg = "This operation is not allowed on a Player that has been deleted";
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += "Player Id: " + playerId.ToString();

            return msg;
        }
    }
}
