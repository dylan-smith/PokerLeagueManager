(function () {
    "use strict";

    function gameController($http) {
        var vm = this;

        vm.gameClicked = function () {
            if (!vm.Visible) {
                $http.get("/api/query/GetGamePlayers?gameId=" + vm.GameId)
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
        vm.GameId = vm.value.GameId;
        vm.Players = vm.value.Players;
        vm.GameDate = vm.value.GameDate;
        vm.Winner = vm.value.Winner;
        vm.Winnings = vm.value.Winnings;
    }

    var module = angular.module("poker");

    module.component("game", {
        templateUrl: "/Scripts/ngComponents/game.component.html",
        bindings: {
            value: "<",
        },
        controllerAs: "vm",
        controller: ["$http", gameController]
    });
}());