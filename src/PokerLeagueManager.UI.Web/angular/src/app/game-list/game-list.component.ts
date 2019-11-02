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

  GamesToLoad: number = 20;
  private mediaMatcher: MediaQueryList = matchMedia(`(max-width: 530px)`);

  constructor(private queryService: QueryService, zone: NgZone) {
    this.mediaMatcher.addListener(() => zone.run(() => this.mediaMatcher = matchMedia(`(max-width: 530px)`)));
  }

  ngOnInit(): void {
    if (this.isScreenSmall()) {
      this.GamesToLoad = 10;
    }

    this.queryService.GetGamesList(0, this.GamesToLoad)
    .subscribe(games => {
        this.Loading = false;
        this.DisableInfiniteScroll = false;
        this.ShowLoadingMore = false;
        this.Games = games;
    });
  }

  LoadMoreGames(): void {
    if (!this.DisableInfiniteScroll) {
      this.DisableInfiniteScroll = true;

      this.queryService.GetGamesList(this.Games.length, this.GamesToLoad)
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
