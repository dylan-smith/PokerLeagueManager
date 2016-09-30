(function () {
    'use strict';

    function gameListController($http, QUERY_URL) {
        /*jshint validthis: true */
        var vm = this;

        $http.get(QUERY_URL + '/api/query/GetGamesList')
             .then(function (response) {
                 vm.Games = response.data;
             });
    }

    var module = angular.module('poker');

    module.component('gameList', {
        templateUrl: '/components/gameList/gameList.component.html',
        controllerAs: 'vm',
        controller: ['$http', 'QUERY_URL', gameListController]
    });
}());