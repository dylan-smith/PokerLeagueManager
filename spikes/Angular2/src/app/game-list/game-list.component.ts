import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { QueryService, IGetGamesListDto } from '../query.service'

@Component({
  selector: 'poker-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.less']
})
export class GameListComponent implements OnInit  {
  
  Loading: boolean = true;
  DisableInfiniteScroll: boolean = true;
  ShowLoadingMore: boolean = false;
  Games: IGetGamesListDto[];

  GamesToLoad: number = 20;

  constructor(private queryService: QueryService, private ngZone: NgZone) {
    

    // if (screenSize.is("xs")) {
    //     vm.GamesToLoad = 10;
    // }
  }

  ngOnInit(): void {
    this.queryService.GetGamesList(0, this.GamesToLoad)
    .subscribe(games => this.ngZone.run(() => {
        this.Loading = false;
        this.DisableInfiniteScroll = false;
        this.ShowLoadingMore = true;
        this.Games = games;
    }));
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
}