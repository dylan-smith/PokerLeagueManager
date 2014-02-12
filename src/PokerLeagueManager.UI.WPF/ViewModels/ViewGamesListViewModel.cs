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
        private IQueryService _queryService;
        private ObservableCollection<GetGamesListDto> _games;

        public ViewGamesListViewModel(IQueryService queryService)
        {
            _queryService = queryService;

            _games = new ObservableCollection<GetGamesListDto>(_queryService.GetGamesList());

            AddGameCommand = new RelayCommand(x => this.AddGame());
        }

        public IEnumerable<string> Games
        {
            get
            {
                return _games.OrderByDescending(g => g.GameDate)
                             .Select(g => string.Format("{0} - {1} [${2}]", g.GameDate.ToString("dd-mmm-yyyy"), g.Winner, g.NumPlayers));
            }
        }

        public System.Windows.Input.ICommand AddGameCommand { get; set; }

        private void AddGame()
        {
            // var addGameView = Resolver.Container.Resolve<EnterGameResultsView>();

            // show the view
        }
    }
}
