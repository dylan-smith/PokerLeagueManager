(function () {
    'use strict';

    /*global pokerConfig */
    var pokerModule = angular.module('poker', ['ngComponentRouter',
                                               'ngAnimate',
                                               'ui.bootstrap',
                                               'ApplicationInsightsModule'])
           .value('$routerRootComponent', 'pokerApp')
           .constant('QUERY_URL', pokerConfig.queryServiceUrl)
           .constant('COMMAND_URL', pokerConfig.commandServiceUrl)
           .config(function (applicationInsightsServiceProvider) {
               applicationInsightsServiceProvider.configure(pokerConfig.appInsightsKey);
           });
}());