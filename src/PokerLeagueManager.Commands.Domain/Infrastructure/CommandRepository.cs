using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class CommandRepository : ICommandRepository
    {
        private IDatabaseLayer _databaseLayer;

        public CommandRepository(IDatabaseLayer databaseLayer)
        {
            _databaseLayer = databaseLayer;
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        public void LogCommand(Common.Commands.Infrastructure.ICommand command)
        {
            _databaseLayer.ExecuteNonQuery(
                "INSERT INTO Commands(CommandId, CommandDateTime, UserName, IpAddress) VALUES(@CommandId, @CommandDateTime, @UserName, @IpAddress)",
                "@CommandId", command.CommandId.ToString(),
                "@CommandDateTime", command.Timestamp.ToUniversalTime().ToString("dd-MMM-yyyy HH:mm:ss.ff"),
                "@UserName", command.User,
                "@IpAddress", command.IPAddress);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "For the DatabaseLayer calls this makes more sense.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
        public void LogFailedCommand(Common.Commands.Infrastructure.ICommand command, Exception ex)
        {
            _databaseLayer.ExecuteNonQuery(
                "UPDATE Commands SET ExceptionDetails = @ExceptionDetails WHERE CommandId = @CommandId",
                "@ExceptionDetails", ex.ToString(),
                "@CommandId", command.CommandId.ToString());
        }
    }
}
