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
        private httpService: ng.IHttpService;
        private queryUrl: string;

        constructor($http: ng.IHttpService, QUERY_URL: string, screenSize: angular.matchmedia.IScreenSize) {
            let vm = this;

            vm.httpService = $http;
            vm.queryUrl = QUERY_URL;

            vm.Loading = true;
            vm.DisableInfiniteScroll = true;
            vm.ShowLoadingMore = false;
            vm.GamesToLoad = 20;

            if (screenSize.is("xs")) {
                this.GamesToLoad = 10;
            }

            $http.post<IGetGamesListDto[]>(QUERY_URL + "/GetGamesList", { Take: this.GamesToLoad })
                .then((response) => {
                    vm.Loading = false;
                    vm.DisableInfiniteScroll = false;
                    vm.ShowLoadingMore = true;
                    vm.Games = response.data;
                });
        }

        public LoadMoreGames(): void {
            if (!this.DisableInfiniteScroll) {
                this.DisableInfiniteScroll = true;

                this.httpService.post<IGetGamesListDto[]>(this.queryUrl + "/GetGamesList",
                                                        { Skip: this.Games.length, Take: this.GamesToLoad })
                    .then((response) => {
                        this.Games = this.Games.concat(response.data);
                        if (response.data.length > 0) {
                            this.DisableInfiniteScroll = false;
                        } else {
                            this.ShowLoadingMore = false;
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
            this.controller = ["$http", "QUERY_URL", "screenSize", GameListController];
            this.templateUrl = "/components/gameList/gameList.component.html";
        }

    }

    angular.module("poker").component("gameList", new GameListComponent());
}
