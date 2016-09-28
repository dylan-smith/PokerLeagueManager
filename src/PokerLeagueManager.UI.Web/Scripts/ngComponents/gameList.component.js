(function () {
    "use strict";

    function gameListController($http) {
        var vm = this;

        $http.get("/api/query/GetGamesList")
            .then(vm.Games = response.data);
    }

    var module = angular.module("poker");

    module.component("gameList", {
        templateUrl: "/Scripts/ngComponents/gameList.component.html",
        controllerAs: "vm",
        controller: ["$http", gameListController]
    });
}());