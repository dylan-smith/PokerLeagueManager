(function () {
    "use strict";

    function gameController($http) {
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Visible) {
                $http.get("/api/query/GetGamePlayers?gameId=" + vm.game.GameId)
                     .then(function (response) {
                         vm.Players = response.data;
                         vm.Visible = true;
                     });
            }
            else {
                vm.Visible = false;
            }
        }

        vm.Visible = false;
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