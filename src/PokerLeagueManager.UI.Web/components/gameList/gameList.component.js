(function () {
    "use strict";

    function gameListController($http) {
        var vm = this;

        $http.get("http://localhost:14271/api/query/GetGamesList")
             .then(function (response) {
                 vm.Games = response.data;
             });
    }

    var module = angular.module("poker");

    module.component("gameList", {
        templateUrl: "/components/gameList/gameList.component.html",
        controllerAs: "vm",
        controller: ["$http", gameListController]
    });
}());