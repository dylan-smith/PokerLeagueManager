/// <reference path="../typings/index.d.ts"/>

module app {
    declare var pokerConfig: IPokerConfig;

    angular.module("poker", ["ngComponentRouter",
                             "ngAnimate",
                             "ui.bootstrap",
                             "infinite-scroll",
                             "matchMedia"])
           .value("$routerRootComponent", "pokerApp")
           .constant("QUERY_URL", pokerConfig.queryServiceUrl)
           .constant("COMMAND_URL", pokerConfig.commandServiceUrl);
}
