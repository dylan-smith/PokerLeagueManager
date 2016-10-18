﻿/// <reference path="../../typings/index.d.ts"/>
/// <reference path="../../model/index.d.ts"/>

module app {
    declare var appInsights: Client;

    interface IGameController {
        LoadingPlayers: boolean;
        Expanded: boolean;
        game: IGetGamesListDto;
        Players: IGetGamePlayersDto[];
        GameClicked(): void;
    }

    class GameController implements IGameController {
        public LoadingPlayers: boolean;
        public Expanded: boolean;
        public game: IGetGamesListDto;
        public Players: IGetGamePlayersDto[];

        private httpService: ng.IHttpService;
        private queryUrl: string;
        private timeoutService: ng.ITimeoutService;

        constructor($http: ng.IHttpService, QUERY_URL: string, $timeout: ng.ITimeoutService) {
            let vm = this;

            vm.httpService = $http;
            vm.queryUrl = QUERY_URL;
            vm.timeoutService = $timeout;
            vm.Expanded = false;
            vm.LoadingPlayers = false;
        }

        public GameClicked(): void {
            let vm = this;

            if (!vm.Expanded) {
                if (!this.Players) {
                    vm.LoadingPlayers = true;
                    vm.httpService.post<IGetGamePlayersDto[]>(vm.queryUrl + "/GetGamePlayers",
                                                            { GameId: vm.game.GameId })
                        .then((response) => {
                            vm.Players = response.data;
                            // The timeout is needed otherwise the players HTML table is in some wonky state
                            // when the collapse directive fires. The timeout forces angular to finish processing
                            // the ng-repeat before trying to expand the section.  Stupid Angular!
                            vm.timeoutService<void>(() => {
                                vm.Expanded = true;
                                vm.LoadingPlayers = false;
                            });
                        });
                } else {
                    vm.Expanded = true;
                }

                if (window.hasOwnProperty("appInsights")) {
                    appInsights.trackEvent("GameExpanded",
                        { Game: vm.game.GameId, GameDate: vm.game.GameDate }
                    );
                }
            } else {
                this.Expanded = false;
            }
        }
    }

    class GameComponent implements ng.IComponentOptions {
        public controller: angular.Injectable<angular.IControllerConstructor>;
        public controllerAs: string;
        public templateUrl: string;
        public bindings: { [boundProperty: string]: string };

        constructor() {
            this.controllerAs = "vm";
            this.controller = ["$http", "QUERY_URL", "$timeout", GameController];
            this.templateUrl = "/components/game/game.component.html";
            this.bindings = {
                game: "<",
            };
        }

    }

    angular.module("poker").component("game", new GameComponent());
}
