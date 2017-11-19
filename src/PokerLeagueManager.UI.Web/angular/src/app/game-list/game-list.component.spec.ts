import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MomentModule } from 'angular2-moment';
import { MatProgressSpinnerModule, MatExpansionModule, MatTableModule } from '@angular/material';
import { InfiniteScrollModule } from '../ngx-infinite-scroll/ngx-infinite-scroll';
import { QueryService, IGetGamesListDto } from '../query.service';
import { GameListComponent } from './game-list.component';
import { GameComponent } from '../game/game.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { mock, when, instance, verify, anyString, anything, deepEqual } from 'ts-mockito';

describe('GameListComponent', () => {
  let component: GameListComponent;
  let fixture: ComponentFixture<GameListComponent>;

  let testGame1: IGetGamesListDto = {
    GameId: '123',
    GameDate: '2017-09-15T15:53:00',
    Winnings: 50,
    Winner: 'Homer Simpson'
  };

  let testGame2: IGetGamesListDto = {
    GameId: '456',
    GameDate: '2017-03-03T21:23',
    Winnings: 300,
    Winner: 'Rick Sanchez'
  }

  let testGames: IGetGamesListDto[] = [
    testGame1,
    testGame2
  ];

  let mockQueryService: QueryService;
  let mockAppInsightsService: AppInsightsService;

  beforeEach(async(() => {
    mockQueryService = mock(QueryService);
    mockAppInsightsService = mock(AppInsightsService);

    when(mockQueryService.GetGamesList(anything(), anything())).thenReturn(Observable.from([testGames]));

    TestBed.configureTestingModule({
      declarations: [ GameListComponent, GameComponent ],
      imports: [
        MatProgressSpinnerModule,
        InfiniteScrollModule,
        MatExpansionModule,
        MatTableModule,
        BrowserAnimationsModule,
        MomentModule
      ],
      providers: [
        { provide: QueryService, useValue: instance(mockQueryService) },
        { provide: AppInsightsService, useValue: instance(mockAppInsightsService) }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameListComponent);
    component = fixture.componentInstance;

    // component.game = testGame;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
