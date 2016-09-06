using System.Data;

namespace PokerLeagueManager.Common.Infrastructure
{
    public interface IDtoFactory
    {
        T Create<T>(DataRow row)
            where T : IDataTransferObject;
    }
}
