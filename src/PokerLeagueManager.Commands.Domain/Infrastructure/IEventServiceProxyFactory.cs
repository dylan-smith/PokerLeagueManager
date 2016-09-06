using System.Data;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventServiceProxyFactory
    {
        IEventServiceProxy Create(DataRow row);
    }
}
