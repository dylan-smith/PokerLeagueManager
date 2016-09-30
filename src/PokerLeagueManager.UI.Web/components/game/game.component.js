(function () {
    'use strict';

    function gameController($http, QUERY_URL) {
        /*jshint validthis: true */
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Expanded) {
                $http.get(QUERY_URL + '/api/query/GetGamePlayers?gameId=' + vm.game.GameId)
                     .then(function (response) {
                         vm.Players = response.data;
                         vm.Expanded = true;
                     });
            }
            else {
                vm.Expanded = false;
            }
        };

        vm.Expanded = false;
    }

    var module = angular.module('poker');

    module.component('game', {
        templateUrl: '/components/game/game.component.html',
        bindings: {
            game: '<',
        },
        controllerAs: 'vm',
        controller: ['$http', 'QUERY_URL', gameController]
    });
}());