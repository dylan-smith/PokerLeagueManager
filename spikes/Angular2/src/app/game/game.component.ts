import { Component, OnInit, Input } from '@angular/core';
import { QueryService, IGetGamesListDto, IGetGamePlayersDto } from '../query.service'
import {DataSource} from '@angular/cdk/collections';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/of';

@Component({
  selector: 'poker-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss']
})
export class GameComponent implements OnInit {
  @Input() game: IGetGamesListDto;
  Players: PlayersDataSource;

  displayedColumns = ['Placing', 'PlayerName', 'Winnings', 'PayIn'];

  constructor(private queryService: QueryService) {
  }

  public GameClicked(): void {
    if (!this.Players) {
      this.queryService.GetGamePlayers(this.game.GameId)
      .subscribe(players => {
        this.Players = new PlayersDataSource(
                            players.sort(function(a, b) {
                              return a.Placing - b.Placing
                            }))});
    }

    // if (window.hasOwnProperty("appInsights")) {
    //     appInsights.trackEvent("GameExpanded",
    //         { Game: this.game.GameId, GameDate: this.game.GameDate }
    //     );
    // }
  }

  ngOnInit() {
  }
}

export class PlayersDataSource extends DataSource<any> {
  constructor(private data: IGetGamePlayersDto[]) {
    super();
  }

  /** Connect function called by the table to retrieve one stream containing the data to render. */
  connect(): Observable<IGetGamePlayersDto[]> {
    return Observable.of(this.data);
  }

  disconnect() {}
}
