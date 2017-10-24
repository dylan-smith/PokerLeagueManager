import { Component, OnInit, Input } from '@angular/core';
import { QueryService, IGetGamesListDto, IGetGamePlayersDto } from '../query.service'

@Component({
  selector: 'poker-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.less']
})
export class GameComponent implements OnInit {
  public LoadingPlayers: boolean;
  public Expanded: boolean;
  @Input() game: IGetGamesListDto;
  public Players: IGetGamePlayersDto[];

  constructor(private queryService: QueryService) {
    this.Expanded = false;
    this.LoadingPlayers = false;
  }

  public GameClicked(): void {
    if (!this.Expanded) {
        if (!this.Players) {
            this.LoadingPlayers = true;
            this.queryService.GetGamePlayers(this.game.GameId)
                .subscribe(players => {
                    this.Players = players;
                    this.Expanded = true;
                    this.LoadingPlayers = false;
                    // The timeout is needed otherwise the players HTML table is in some wonky state
                    // when the collapse directive fires. The timeout forces angular to finish processing
                    // the ng-repeat before trying to expand the section.  Stupid Angular!
                    // this.timeoutService<void>(() => {
                    //     this.Expanded = true;
                    //     this.LoadingPlayers = false;
                    // });
                });
        } else {
            this.Expanded = true;
        }

        // if (window.hasOwnProperty("appInsights")) {
        //     appInsights.trackEvent("GameExpanded",
        //         { Game: this.game.GameId, GameDate: this.game.GameDate }
        //     );
        // }
    } else {
        this.Expanded = false;
    }
}

  ngOnInit() {
  }

}