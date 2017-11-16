import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MomentModule } from 'angular2-moment';
import { MatExpansionModule, MatTableModule } from '@angular/material';
import { QueryService, IGetGamePlayersDto, IGetGamesListDto } from '../query.service';
import { GameComponent } from './game.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { click } from '../shared/testHelpers';
import { DebugElement } from '@angular/core';
import { mock, when, instance, verify, anyString, anything, deepEqual } from 'ts-mockito';

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

  let mockQueryService: QueryService;
  let mockAppInsightsService: AppInsightsService;

  beforeEach(async(() => {
    mockQueryService = mock(QueryService);
    mockAppInsightsService = mock(AppInsightsService);

    when(mockQueryService.GetGamePlayers(testGame.GameId)).thenReturn(Observable.from([testGamePlayers]));

    TestBed.configureTestingModule({
      declarations: [ GameComponent ],
      imports: [
        MomentModule,
        MatExpansionModule,
        MatTableModule,
        BrowserAnimationsModule
      ],
      providers: [
        { provide: QueryService, useValue: instance(mockQueryService) },
        { provide: AppInsightsService, useValue: instance(mockAppInsightsService) }
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

  it('should set game date with proper format', () => {
    let gameTitle = fixture.debugElement.query(By.css('.game-date')).nativeElement;
    expect(gameTitle.textContent).toBe('September 15, 2017');
  });

  it('should set game winner and winnings in panel header', () => {
    let gameWinner = fixture.debugElement.query(By.css('.game-winner')).nativeElement;
    expect(gameWinner.textContent).toBe('Homer Simpson ($50)');
  });

  describe('on a small screen', () => {
    beforeEach(() => {
      spyOn(window, 'matchMedia').and.returnValue({ matches: true });
      fixture.detectChanges();
    });

    it('isScreenSmall should return true', () => {
      async(() => {
        expect(component.isScreenSmall()).toBeTruthy();
      });
    });

    it('should set column title to Win', () => {
      let headerRow = fixture.debugElement.query(By.css('.mat-header-row'));
      let winningsHeader = headerRow.query(By.css('.mat-column-Winnings')).nativeElement;

      async(() => {
        expect(winningsHeader.textContent).toBe('Win');
      })
    });

    it('should set column title to Pay', () => {
      let headerRow = fixture.debugElement.query(By.css('.mat-header-row'));
      let payInHeader = headerRow.query(By.css('.mat-column-PayIn')).nativeElement;

      async(() => {
        expect(payInHeader.textContent).toBe('Pay');
      })
    });
  });

  describe('on a large screen', () => {
    beforeEach(() => {
      spyOn(window, 'matchMedia').and.returnValue({ matches: false });
      fixture.detectChanges();
    });

    it('isScreenSmall should return false', () => {
      async(() => {
        expect(component.isScreenSmall()).toBeFalsy();
      });
    });

    it('should set column title to Winnings', () => {
      let headerRow = fixture.debugElement.query(By.css('.mat-header-row'));
      let winningsHeader = headerRow.query(By.css('.mat-column-Winnings')).nativeElement;

      async(() => {
        expect(winningsHeader.textContent).toBe('Winnings');
      })
    });

    it('should set column title to Pay In', () => {
      let headerRow = fixture.debugElement.query(By.css('.mat-header-row'));
      let payInHeader = headerRow.query(By.css('.mat-column-PayIn')).nativeElement;

      async(() => {
        expect(payInHeader.textContent).toBe('Pay In');
      })
    });
  });

  describe('when expanded', () => {
    let spy: jasmine.Spy;
    let gameHeader: DebugElement;

    beforeEach(() => {
      spyOn(component, 'GameExpanded').and.callThrough();

      gameHeader = fixture.debugElement.query(By.css('mat-expansion-panel-header'));
      click(gameHeader);

      fixture.detectChanges();
    });

    it('should call GameExpanded', () => {
      expect(component.GameExpanded).toHaveBeenCalled();
    })

    it('should call GetGamePlayers with the gameId', () => {
      verify(mockQueryService.GetGamePlayers(testGame.GameId)).called();
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
      verify(mockAppInsightsService.trackEvent('GameExpanded', deepEqual({ 'GameId': testGame.GameId, 'GameDate': testGame.GameDate }))).called();
    });

    describe('then collapsed', () => {
      beforeEach(() => {
        click(gameHeader);
      });

      it('should not call GameExpanded', () => {
        expect(component.GameExpanded).toHaveBeenCalledTimes(1);
      });

      it ('should not send an AppInsights event', () => {
        verify(mockAppInsightsService.trackEvent(anything(), anything())).once();
      });

      describe('then expanded again', () => {
        beforeEach(() => {
          click(gameHeader);
        });

        it('should use cached players list', () => {
          expect(component.GameExpanded).toHaveBeenCalledTimes(2);
          verify(mockQueryService.GetGamePlayers(anyString())).once();
        });

        it('should send another AppInsights event', () => {
          verify(mockAppInsightsService.trackEvent(anything(), anything())).twice();
        });
      })
    });
  });
});
