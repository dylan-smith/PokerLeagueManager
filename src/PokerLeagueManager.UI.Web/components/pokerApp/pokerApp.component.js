(function () {
    'use strict';

    var module = angular.module('poker');

    module.component('pokerApp', {
        templateUrl: '/components/pokerApp/pokerApp.component.html',
        $routeConfig: [
            { path: '/', component: 'gameList', name: 'Home' },
            { path: '/**', redirectTo: ['Home', ''] }
        ]
    });
}());