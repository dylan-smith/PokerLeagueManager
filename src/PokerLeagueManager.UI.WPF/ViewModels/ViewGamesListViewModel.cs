using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class ViewGamesListViewModel : BaseViewModel, INotifyPropertyChanged, IViewGamesListViewModel
    {
        private ObservableCollection<GetGamesListDto> _games;

        public ViewGamesListViewModel(IQueryService queryService, IMainWindow mainWindow)
            : base(null, queryService, mainWindow)
        {
            _games = new ObservableCollection<GetGamesListDto>(_QueryService.GetGamesList());

            AddGameCommand = new RelayCommand(x => this.AddGame());

            Height = 400;
            Width = 385;
            WindowTitle = "View Games";
        }

        public IEnumerable<string> Games
        {
            get
            {
                return _games.OrderByDescending(g => g.GameDate)
                             .Select(g => string.Format("{0} - {1} [${2}]", g.GameDate.ToString("dd-MMM-yyyy"), g.Winner, g.Winnings));
            }
        }

        public System.Windows.Input.ICommand AddGameCommand { get; set; }

        private void AddGame()
        {
            var view = Resolver.Container.Resolve<IEnterGameResultsView>();
            _MainWindow.ShowView(view);
        }
    }
}
