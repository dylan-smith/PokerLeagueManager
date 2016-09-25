(function () {
    "use strict";

    var module = angular.module("poker");

    module.component("pokerApp", {
        templateUrl: "/Scripts/ngComponents/pokerApp.component.html",
        $routeConfig: [
            { path: "/", component: "gameList", name: "Home" },
            { path: "/**", redirectTo: ["Home", ""] }
        ]
    });
}());