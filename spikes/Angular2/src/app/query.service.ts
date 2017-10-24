import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { IGetGameCountByDateDto } from './dtos/IGetGameCountByDateDto';
import { IGetGamePlayersDto } from './dtos/IGetGamePlayersDto';
import { IGetGamesListDto } from './dtos/IGetGamesListDto';
import { IGetGamesWithPlayerDto } from './dtos/IGetGamesWithPlayerDto';
import { IGetPlayerByNameDto } from './dtos/IGetPlayerByNameDto';
import { IGetPlayerGamesDto } from './dtos/IGetPlayerGamesDto';
import { IGetPlayerStatisticsDto } from './dtos/IGetPlayerStatisticsDto';

export { IGetGameCountByDateDto } from './dtos/IGetGameCountByDateDto';
export { IGetGamePlayersDto } from './dtos/IGetGamePlayersDto';
export { IGetGamesListDto } from './dtos/IGetGamesListDto';
export { IGetGamesWithPlayerDto } from './dtos/IGetGamesWithPlayerDto';
export { IGetPlayerByNameDto } from './dtos/IGetPlayerByNameDto';
export { IGetPlayerGamesDto } from './dtos/IGetPlayerGamesDto';
export { IGetPlayerStatisticsDto } from './dtos/IGetPlayerStatisticsDto';

@Injectable()
export class QueryService {

  constructor(private _http: HttpClient, @Inject('QUERY_URL') private QUERY_URL: string) { }

  public GetGameCountByDate(GameDate: string): Observable<number> {
    return this._http.post<number>(this.QUERY_URL + "/GetGameCountByDate", { GameDate });
  }

  public GetGamePlayers(GameId: string): Observable<IGetGamePlayersDto[]> {
    return this._http.post<IGetGamePlayersDto[]>(this.QUERY_URL + "/GetGamePlayers", { GameId });
  }

public GetGamesList(Skip: number, Take: number): Observable<IGetGamesListDto[]> {
    return this._http.post<IGetGamesListDto[]>(this.QUERY_URL + "/GetGamesList", { Skip, Take });
  }

public GetGamesWithPlayer(PlayerName: string): Observable<IGetGamesWithPlayerDto[]> {
    return this._http.post<IGetGamesWithPlayerDto[]>(this.QUERY_URL + "/GetGamesWithPlayer", { PlayerName });
  }

public GetPlayerByName(PlayerName: string): Observable<IGetPlayerByNameDto> {
    return this._http.post<IGetPlayerByNameDto>(this.QUERY_URL + "/GetPlayerByName", { PlayerName });
  }

public GetPlayerGames(PlayerName: string): Observable<IGetPlayerGamesDto[]> {
    return this._http.post<IGetPlayerGamesDto[]>(this.QUERY_URL + "/GetPlayerGames", { PlayerName });
  }

public GetPlayerStatistics(): Observable<IGetPlayerStatisticsDto[]> {
    return this._http.post<IGetPlayerStatisticsDto[]>(this.QUERY_URL + "/GetPlayerStatistics", {  });
  }
}