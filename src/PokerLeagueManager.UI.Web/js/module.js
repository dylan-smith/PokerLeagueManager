(function () {
    'use strict';

    var host = location.host;
    var commandUrl = '';
    var queryUrl = '';

    if (host.toLowerCase().includes('pokerleaguemanager.net')) {
        commandUrl = 'http://command.pokerleaguemanager.net';
        queryUrl = 'http://query.pokerleaguemanager.net';
    }

    if (host.toLowerCase().includes('azurewebsites.net')) {
        commandUrl = 'http://command-' + host;
        queryUrl = 'http://query-' + host;
    }

    if (host.toLowerCase().includes('localhost')) {
        commandUrl = 'http://localhost:?????';
        queryUrl = 'http://localhost:14271';
    }

    angular.module('poker', ['ngComponentRouter'])
           .value('$routerRootComponent', 'pokerApp')
           .constant('QUERY_URL', queryUrl)
           .constant('COMMAND_URL', commandUrl);
}());