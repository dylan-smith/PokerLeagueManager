(function () {
    "use strict";

    function gameListController($http) {
        var vm = this;

        var gamesReturned = function (response) {
            vm.Games = response.data;
        }

        $http.get("/api/query/GetGamesList")
            .then(gamesReturned);
    }

    var module = angular.module("poker");

    module.component("gameList", {
        templateUrl: "/Scripts/ngComponents/gameList.component.html",
        controllerAs: "vm",
        controller: ["$http", gameListController]
    });
}());