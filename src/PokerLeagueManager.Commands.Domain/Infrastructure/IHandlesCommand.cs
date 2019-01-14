using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IHandlesCommand<in T>
        where T : ICommand
    {
        IEventRepository Repository { get; set; }

        IQueryService QueryService { get; set; }

        void Execute(T command);
    }
}
