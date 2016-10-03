(function () {
    'use strict';

    var host = location.host;
    var commandUrl = '';
    var queryUrl = '';

    if (host.toLowerCase().includes('pokerleaguemanager.net')) {
        commandUrl = 'http://commands.pokerleaguemanager.net';
        queryUrl = 'http://queries.pokerleaguemanager.net';
    }

    if (host.toLowerCase().includes('azurewebsites.net')) {
        commandUrl = 'http://commands-' + host;
        queryUrl = 'http://queries-' + host;
    }

    if (host.toLowerCase().includes('localhost')) {
        commandUrl = 'http://localhost:4224';
        queryUrl = 'http://localhost:14271';
    }

    angular.module('poker', ['ngComponentRouter', 'ngAnimate'])
           .value('$routerRootComponent', 'pokerApp')
           .constant('QUERY_URL', queryUrl)
           .constant('COMMAND_URL', commandUrl);
}());