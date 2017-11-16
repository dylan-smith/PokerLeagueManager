import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MomentModule } from 'angular2-moment';
import { MatButtonModule, MatExpansionModule, MatTableModule } from '@angular/material';
import { QueryService, IGetGamePlayersDto, IGetGamesListDto } from '../query.service';

import { GameComponent } from './game.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { click } from '../shared/testHelpers';
import { DebugElement } from '@angular/core';

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
    player2,
    player1
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

  describe('when expanded', () => {
    let spy: jasmine.Spy;
    let queryServiceStub: QueryServiceStub;
    let appInsightsServiceStub: AppInsightsServiceStub;
    let gameHeader: DebugElement;

    beforeEach(() => {
      queryServiceStub = fixture.debugElement.injector.get(QueryService);
      appInsightsServiceStub = fixture.debugElement.injector.get(AppInsightsService);
      spyOn(queryServiceStub, 'GetGamePlayers').and.returnValue(Observable.from([testGamePlayers]));
      spyOn(appInsightsServiceStub, 'trackEvent');
      spyOn(component, 'GameClicked').and.callThrough();

      gameHeader = fixture.debugElement.query(By.css('mat-expansion-panel-header'));
      click(gameHeader);

      fixture.detectChanges();
    });

    it('should call GameClicked', () => {
      expect(component.GameClicked).toHaveBeenCalled();
    })

    it('should call GetGamePlayers with the gameId', () => {
      expect(queryServiceStub.GetGamePlayers).toHaveBeenCalledWith(testGame.GameId);
    });

    it('should create 2 table rows', () => {
      let rowCount = fixture.debugElement.queryAll(By.css('.mat-row')).length;

      expect(rowCount).toBe(2);
    });

    it('should have the first row set to the winner', () => {
      let row = fixture.debugElement.queryAll(By.css('.mat-row'))[0];

      let placing = row.query(By.css('.mat-column-Placing')).nativeElement.textContent;
      let playerName = row.query(By.css('.mat-column-PlayerName')).nativeElement.textContent;
      let winnings = row.query(By.css('.mat-column-Winnings')).nativeElement.textContent;
      let payIn = row.query(By.css('.mat-column-PayIn')).nativeElement.textContent;

      expect(placing).toBe(player1.Placing.toString(), 'Placing');
      expect(playerName).toBe(player1.PlayerName, 'PlayerName');
      expect(winnings).toBe('$' + player1.Winnings.toString(), 'Winnings');
      expect(payIn).toBe('$' + player1.PayIn.toString(), 'PayIn');
    });

    it('should have the second row set to the loser', () => {
      let row = fixture.debugElement.queryAll(By.css('.mat-row'))[1];

      let placing = row.query(By.css('.mat-column-Placing')).nativeElement.textContent;
      let playerName = row.query(By.css('.mat-column-PlayerName')).nativeElement.textContent;
      let winnings = row.query(By.css('.mat-column-Winnings')).nativeElement.textContent;
      let payIn = row.query(By.css('.mat-column-PayIn')).nativeElement.textContent;

      expect(placing).toBe(player2.Placing.toString(), 'Placing');
      expect(playerName).toBe(player2.PlayerName, 'PlayerName');
      expect(winnings).toBe('$' + player2.Winnings.toString(), 'Winnings');
      expect(payIn).toBe('$' + player2.PayIn.toString(), 'PayIn');
    });

    it('should send appInsights event', () => {
      expect(appInsightsServiceStub.trackEvent).toHaveBeenCalledWith('GameExpanded', { 'GameId': testGame.GameId, 'GameDate': testGame.GameDate });
    });

    describe('then collapsed', () => {
      beforeEach(() => {
        click(gameHeader);
      });

      it('should not call GameClicked', () => {
        expect(component.GameClicked).toHaveBeenCalledTimes(1);
      });

      describe('then expanded again', () => {
        beforeEach(() => {
          click(gameHeader);
          fixture.detectChanges();
        });

        it('should use cached players list', () => {
          expect(component.GameClicked).toHaveBeenCalledTimes(2);
          expect(queryServiceStub.GetGamePlayers).toHaveBeenCalledTimes(1);
        })
      })
    });
  });
  // test that the column header text changes on small screens
  // test that player list is cached on multiple clicks
  // test that appinsights event is sent on every expand (but not collapse)
});
