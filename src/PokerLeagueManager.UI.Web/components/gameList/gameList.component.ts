/// <reference path="../../typings/index.d.ts"/>
/// <reference path="../../model/index.d.ts"/>

module app {
    interface IGameListController {
        Loading: boolean;
        DisableInfiniteScroll: boolean;
        ShowLoadingMore: boolean;
        Games: IGetGamesListDto[];
        LoadMoreGames(): void;
    }

    class GameListController implements IGameListController {
        public Loading: boolean;
        public DisableInfiniteScroll: boolean;
        public ShowLoadingMore: boolean;
        public Games: IGetGamesListDto[];

        private GamesToLoad: number;

        constructor(private queryService: QueryService, screenSize: angular.matchmedia.IScreenSize) {
            let vm = this;

            vm.Loading = true;
            vm.DisableInfiniteScroll = true;
            vm.ShowLoadingMore = false;
            vm.GamesToLoad = 20;

            if (screenSize.is("xs")) {
                vm.GamesToLoad = 10;
            }

            queryService.GetGamesList(0, vm.GamesToLoad)
                .then(games => {
                    vm.Loading = false;
                    vm.DisableInfiniteScroll = false;
                    vm.ShowLoadingMore = true;
                    vm.Games = games;
                });
        }

        public LoadMoreGames(): void {
            let vm = this;

            if (!vm.DisableInfiniteScroll) {
                vm.DisableInfiniteScroll = true;

                vm.queryService.GetGamesList(vm.Games.length, vm.GamesToLoad)
                    .then(games => {
                        vm.Games = vm.Games.concat(games);
                        if (games.length > 0) {
                            vm.DisableInfiniteScroll = false;
                        } else {
                            vm.ShowLoadingMore = false;
                        }
                    });
            }
        }
    }

    class GameListComponent implements ng.IComponentOptions {
        public controller: angular.Injectable<angular.IControllerConstructor>;
        public controllerAs: string;
        public templateUrl: string;

        constructor() {
            this.controllerAs = "vm";
            this.controller = ["QueryService", "screenSize", GameListController];
            this.templateUrl = "/components/gameList/gameList.component.html";
        }

    }

    angular.module("poker").component("gameList", new GameListComponent());
}
