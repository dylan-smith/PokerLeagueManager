(function () {
    'use strict';

    function gameController($http, QUERY_URL, $timeout) {
        /*jshint validthis: true */
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Expanded) {
                if (!vm.Players) {
                    vm.LoadingPlayers = true;
                    $http.post(QUERY_URL + '/GetGamePlayers', { GameId: vm.game.GameId })
                        .then(function (response) {
                            vm.Players = response.data;
                            // The timeout is needed otherwise the players HTML table is in some wonky state
                            // when the collapse directive fires. The timeout forces angular to finish processing
                            // the ng-repeat before trying to expand the section.  Stupid Angular!
                            $timeout(function () {
                                vm.Expanded = true;
                                vm.LoadingPlayers = false;
                            });
                        });
                } else {
                    vm.Expanded = true;
                }

                appInsights.trackEvent("GameExpanded",
                    { Game: vm.game.GameId, GameDate: vm.game.GameDate }
                );
            }
            else {
                vm.Expanded = false;
            }
        };

        vm.Expanded = false;
        vm.LoadingPlayers = false;
    }

    var module = angular.module('poker');

    module.component('game', {
        templateUrl: '/components/game/game.component.html',
        bindings: {
            game: '<',
        },
        controllerAs: 'vm',
        controller: ['$http', 'QUERY_URL', '$timeout', gameController]
    });
}());