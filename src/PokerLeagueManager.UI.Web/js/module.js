(function () {
    'use strict';

    var pokerModule = angular.module('poker', ['ngComponentRouter', 'ngAnimate', 'ui.bootstrap', 'ApplicationInsightsModule'])
           .value('$routerRootComponent', 'pokerApp')
           .constant('QUERY_URL', pokerConfig.queryServiceUrl)
           .constant('COMMAND_URL', pokerConfig.commandServiceUrl);

    pokerModule.config(['applicationInsightsServiceProvider'], function (applicationInsightsServiceProvider) {
        applicationInsightsServiceProvider.configure(pokerConfig.appInsightsKey);
    });
}());