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
  player1: IGetGamePlayersDto = {
    GameId: '123',
    PlayerName: 'Homer Simpson',
    Placing: 1,
    Winnings: 50,
    PayIn: 20
  };

  player2: IGetGamePlayersDto = {
    GameId: '123',
    PlayerName: 'Bart Simpson',
    Placing: 2,
    Winnings: 0,
    PayIn: 30
  };

  testGamePlayers: IGetGamePlayersDto[] = [
    this.player1,
    this.player2
  ];

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
    let testGame: IGetGamesListDto = {
      GameId: '123',
      GameDate: '2017-09-15T15:53:00',
      Winnings: 50,
      Winner: 'Homer Simpson'
    };

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

  // test that the column header text changes on small screens
});
