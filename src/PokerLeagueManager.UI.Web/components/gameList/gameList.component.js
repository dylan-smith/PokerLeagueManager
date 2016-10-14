(function () {
    'use strict';

    function gameListController($http, QUERY_URL, screenSize) {
        /*jshint validthis: true */
        var vm = this;
        vm.Loading = true;
        vm.DisableInfiniteScroll = true;
        vm.ShowLoadingMore = false;

        vm.GamesToLoad = 20;

        if (screenSize.is('xs')) {
            vm.GamesToLoad = 10;
        }

        $http.post(QUERY_URL + '/GetGamesList', { Take: vm.GamesToLoad })
             .then(function (response) {
                 vm.Loading = false;
                 vm.DisableInfiniteScroll = false;
                 vm.ShowLoadingMore = true;
                 vm.Games = response.data;
             });

        vm.LoadMoreGames = function () {
            if (!vm.DisableInfiniteScroll) {
                vm.DisableInfiniteScroll = true;

                $http.post(QUERY_URL + '/GetGamesList', { Skip: vm.Games.length, Take: vm.GamesToLoad })
                 .then(function (response) {
                     vm.Games = vm.Games.concat(response.data);
                     if (response.data.length > 0) {
                         vm.DisableInfiniteScroll = false;
                     } else {
                         vm.ShowLoadingMore = false;
                     }
                 });
            }
        };
    }

    var module = angular.module('poker');

    module.component('gameList', {
        templateUrl: '/components/gameList/gameList.component.html',
        controllerAs: 'vm',
        controller: ['$http', 'QUERY_URL', 'screenSize', gameListController]
    });
}());