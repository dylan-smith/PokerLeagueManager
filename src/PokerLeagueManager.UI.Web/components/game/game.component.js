(function () {
    'use strict';

    function gameController($http, QUERY_URL) {
        /*jshint validthis: true */
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Expanded) {
                $http.post(QUERY_URL + '/GetGamePlayers', { GameId: vm.game.GameId })
                    .then(function (response) {
                        vm.Players = response.data;
                        vm.Expanded = true;
                        vm.ExpandedHeight = 37 + vm.Players.length * 37;
                    });
            }
            else {
                vm.Expanded = false;
            }
        };

        vm.Expanded = false;
        vm.ExpandedHeight = 0;
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