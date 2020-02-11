import { Component, OnInit, NgZone } from '@angular/core';
import { QueryService, IGetGamesListDto } from '../query.service'

@Component({
  selector: 'poker-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit {
  Loading: boolean = true;
  DisableInfiniteScroll: boolean = true;
  ShowLoadingMore: boolean = false;
  Games: IGetGamesListDto[];

  mediaMatcher: MediaQueryList = matchMedia(`(max-width: 530px)`);

  constructor(private queryService: QueryService, zone: NgZone) {
    this.mediaMatcher.addListener(() => zone.run(() => this.mediaMatcher = matchMedia(`(max-width: 530px)`)));
  }

  ngOnInit(): void {
    this.queryService.GetGamesList(0, this.GetGamesToLoad())
    .subscribe(games => {
        this.Loading = false;
        this.DisableInfiniteScroll = false;
        this.ShowLoadingMore = true;
        this.Games = games;
    });
  }

  GetGamesToLoad(): number {
    if (this.isScreenSmall()) {
      return 10;
    }

    return 20;
  }

  LoadMoreGames(): void {
    if (!this.DisableInfiniteScroll) {
      this.DisableInfiniteScroll = true;

      this.queryService.GetGamesList(this.Games.length, this.GetGamesToLoad())
        .subscribe(games => {
          this.Games = this.Games.concat(games);
          if (games.length > 0) {
            this.DisableInfiniteScroll = false;
          } else {
            this.ShowLoadingMore = false;
          }
        });
    }
  }

  isScreenSmall(): boolean {
    return this.mediaMatcher.matches;
  }
}
