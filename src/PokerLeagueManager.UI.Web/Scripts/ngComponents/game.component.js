(function () {
    "use strict";

    function gameController($http) {
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Expanded) {
                $http.get("/api/query/GetGamePlayers?gameId=" + vm.game.GameId)
                     .then(function (response) {
                         vm.Players = response.data;
                         vm.Expanded = true;
                     });
            }
            else {
                vm.Expanded = false;
            }
        }

        vm.Expanded = false;
    }

    var module = angular.module("poker");

    module.component("game", {
        templateUrl: "/Scripts/ngComponents/game.component.html",
        bindings: {
            game: "<",
        },
        controllerAs: "vm",
        controller: ["$http", gameController]
    });
}());