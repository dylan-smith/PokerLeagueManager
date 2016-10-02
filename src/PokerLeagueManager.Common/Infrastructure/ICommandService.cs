namespace PokerLeagueManager.Common.Infrastructure
{
    public interface ICommandService
    {
        void ExecuteCommand(ICommand command);
    }
}
