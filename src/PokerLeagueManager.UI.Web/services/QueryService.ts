/// <reference path="../typings/index.d.ts"/>
/// <reference path="../model/index.d.ts"/>

module app {
    interface IQueryService {
        GetGamesList(skip: number, take: number): ng.IPromise<IGetGamesListDto[]>;
        GetGamePlayers(gameId: string): ng.IPromise<IGetGamePlayersDto[]>;
    }

    export class QueryService implements IQueryService {
        constructor(private $http: ng.IHttpService, private QUERY_URL: string) {

        }

        public GetGamesList(skip: number, take: number): ng.IPromise<IGetGamesListDto[]> {
            return this.$http.post<IGetGamesListDto[]>(this.QUERY_URL + "/GetGamesList", { Skip: skip, Take: take })
                .then((response) => {
                    return response.data;
                });
        }

        public GetGamePlayers(gameId: string): ng.IPromise<IGetGamePlayersDto[]> {
            return this.$http.post<IGetGamePlayersDto[]>(this.QUERY_URL + "/GetGamePlayers", { GameId: gameId })
                .then((response) => {
                    return response.data;
                });
        }
    }

    angular.module("poker").service("QueryService", ["$http", "QUERY_URL", QueryService]);
}
