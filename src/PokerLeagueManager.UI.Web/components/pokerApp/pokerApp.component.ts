module app {
    angular.module("poker").component("pokerApp", {
        $routeConfig: [
            { path: "/", component: "gameList", name: "Home" },
            { path: "/Games", component: "gameList", name: "Games" },
            { path: "/Stats", component: "playerStats", name: "Stats" },
            { path: "/POTY", component: "playerOfTheYear", name: "POTY" },
            { path: "/Videos", component: "videos", name: "Videos" },
            { path: "/Pictures", component: "pictures", name: "Pictures" },
            { path: "/Rules", component: "rules", name: "Rules" },
            { path: "/**", redirectTo: ["Home", ""] },
        ],
        templateUrl: "/components/pokerApp/pokerApp.component.html",
    });
}
