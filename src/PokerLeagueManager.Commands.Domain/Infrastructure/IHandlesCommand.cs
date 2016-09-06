using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IHandlesCommand<T>
        where T : ICommand
    {
        IEventRepository Repository { get; set; }

        IQueryService QueryService { get; set; }

        void Execute(T command);
    }
}
