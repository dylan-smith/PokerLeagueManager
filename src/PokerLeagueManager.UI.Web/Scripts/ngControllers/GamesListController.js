var app = angular.module("PokerLeagueManager", []);

var gamesListController = function ($scope, $http) {
    var gamesReturned = function (response) {
        $scope.Games = response.data;

        for (index = 0; index < $scope.Games.length; ++index) {
            $scope.Games[index].Visible = false;
        }
    }

    $http.get("/api/query/GetGamesList")
        .then(gamesReturned);

    $scope.GameClicked = function (game) {
        if (!game.Visible) {
            $http.get("/api/query/GetGamePlayers?gameId=" + game.GameId)
                 .then(function (response) {
                     game.Players = response.data;
                     game.Visible = true;
                 });
        }
        else
        {
            game.Visible = false;
        }
    }
}

app.controller("GamesListController", ["$scope", "$http", gamesListController]);