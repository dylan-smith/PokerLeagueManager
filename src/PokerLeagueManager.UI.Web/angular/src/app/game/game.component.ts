import { Component, OnInit, Input, NgZone } from '@angular/core';
import { QueryService, IGetGamesListDto, IGetGamePlayersDto } from '../query.service'
import {DataSource} from '@angular/cdk/collections';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { AppInsightsService } from '@markpieszak/ng-application-insights';

@Component({
  selector: 'poker-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {
  @Input() game: IGetGamesListDto;
  Players: PlayersDataSource;

  displayedColumns = ['Placing', 'PlayerName', 'Winnings', 'PayIn'];
  private mediaMatcher: MediaQueryList = matchMedia(`(max-width: 530px)`);

  constructor(private queryService: QueryService, private appInsightsService: AppInsightsService, zone: NgZone) {
    this.mediaMatcher.addListener(() => zone.run(() => this.mediaMatcher = matchMedia(`(max-width: 530px)`)));
  }

  public GameExpanded(): void {
    if (!this.Players) {
      this.queryService.GetGamePlayers(this.game.GameId)
      .subscribe(players => {
        this.Players = new PlayersDataSource(
                            players.sort(function(a, b) {
                              return a.Placing - b.Placing
                            }))});
    }

    this.appInsightsService.trackEvent('GameExpanded', { 'GameId': this.game.GameId, 'GameDate': this.game.GameDate });
  }

  ngOnInit() {
  }

  isScreenSmall(): boolean {
    return this.mediaMatcher.matches;
  }
}

export class PlayersDataSource extends DataSource<any> {
  constructor(public data: IGetGamePlayersDto[]) {
    super();
  }

  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<IGetGamePlayersDto[]> {
    return Observable.of(this.data);
  }

  disconnect() {}
}
