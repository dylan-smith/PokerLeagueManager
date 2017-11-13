import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MomentModule } from 'angular2-moment';
import { MatButtonModule, MatExpansionModule, MatSidenavModule, MatIconModule, MatProgressSpinnerModule, MatTableModule } from '@angular/material';
import { QueryService, IGetGamePlayersDto, IGetGamesListDto } from '../query.service';

import { GameComponent } from './game.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { AppInsightsService } from '@markpieszak/ng-application-insights';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
    return Observable.from([this.testGamePlayers]);
  }
}

class AppInsightsServiceStub {
  trackEvent(eventName: string,
             eventProperties?: {[name: string]: string;},
             metricProperty?: {[name: string]: number;}): void {

    // do a spy instead
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
});
