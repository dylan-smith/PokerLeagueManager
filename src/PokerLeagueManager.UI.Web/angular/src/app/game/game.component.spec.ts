import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MomentModule } from 'angular2-moment';
import { MatButtonModule, MatExpansionModule, MatSidenavModule, MatIconModule, MatProgressSpinnerModule, MatTableModule } from '@angular/material';
import { QueryService, IGetGamePlayersDto, IGetGamesListDto } from '../query.service';

import { GameComponent } from './game.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';

class QueryServiceStub {
  GetGamePlayers(GameId: string): Observable<IGetGamePlayersDto[]> {
    return null;
  }
}

class AppInsightsServiceStub {
  trackEvent(eventName: string,
             eventProperties?: {[name: string]: string;},
             metricProperty?: {[name: string]: number;}): void {
  }
}

describe('GameComponent', () => {
  let component: GameComponent;
  let fixture: ComponentFixture<GameComponent>;
  let testGame: IGetGamesListDto = {
    GameId: '123',
    GameDate: '2017-09-15T15:53:00',
    Winnings: 50,
    Winner: 'Homer Simpson'
  };

  let player1: IGetGamePlayersDto = {
    GameId: '123',
    PlayerName: 'Homer Simpson',
    Placing: 1,
    Winnings: 50,
    PayIn: 20
  };

  let player2: IGetGamePlayersDto = {
    GameId: '123',
    PlayerName: 'Bart Simpson',
    Placing: 2,
    Winnings: 0,
    PayIn: 30
  };

  let testGamePlayers: IGetGamePlayersDto[] = [
    this.player2,
    this.player1
  ];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GameComponent ],
      imports: [
        MomentModule,
        MatExpansionModule,
        MatTableModule,
        BrowserAnimationsModule
      ],
      providers: [
        { provide: QueryService, useClass: QueryServiceStub },
        { provide: AppInsightsService, useClass: AppInsightsServiceStub }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameComponent);
    component = fixture.componentInstance;

    component.game = testGame;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('isScreenSmall should return true when mediaQuery matches', () => {
    spyOn(window, 'matchMedia').and.returnValue({ matches: true });
    fixture.detectChanges();
    async(() => {
      expect(component.isScreenSmall()).toBeTruthy();
    });
  });

  it('isScreenSmall should return false when mediaQuery doesnt match', () => {
    spyOn(window, 'matchMedia').and.returnValue({ matches: false });
    fixture.detectChanges();
    async(() => {
      expect(component.isScreenSmall()).toBeFalsy();
    });
  });

  it('should set game date with proper format', () => {
    let gameTitle = fixture.debugElement.query(By.css('.game-date')).nativeElement;

    expect(gameTitle.textContent).toBe('September 15, 2017');
  });

  it('should set game winner and winnings in panel header', () => {
    let gameWinner = fixture.debugElement.query(By.css('.game-winner')).nativeElement;

    expect(gameWinner.textContent).toBe('Homer Simpson ($50)');
  });

  describe('when clicked', () => {
    let spy: jasmine.Spy;
    let queryServiceStub: QueryServiceStub;

    beforeEach(() => {
      queryServiceStub = fixture.debugElement.injector.get(QueryService);
      spy = spyOn(queryServiceStub, 'GetGamePlayers').and.returnValue(Observable.from([testGamePlayers]));

      component.GameClicked();
      fixture.detectChanges();
    });

    it('should call GetGamePlayers with the gameId', () => {
      expect(queryServiceStub.GetGamePlayers).toHaveBeenCalledWith(testGame.GameId);
    });
  });
  // test that the column header text changes on small screens
  // test that player details show up in the table
  // test that player list is cached on multiple clicks
  // test that appinsights event is sent on every expand (but not collapse)
});
