import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatProgressSpinnerModule } from '@angular/material';
import { InfiniteScrollModule } from '../ngx-infinite-scroll/ngx-infinite-scroll';
import { QueryService, IGetGamesListDto } from '../query.service';
import { GameListComponent } from './game-list.component';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/from';
import { mock, when, instance, verify, anyString, anything, deepEqual } from 'ts-mockito';
import { Component, Input } from '@angular/core';

@Component({selector: 'poker-game', template: ''})
class GameStubComponent {
  @Input() game: IGetGamesListDto;
}

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

  beforeEach(async(() => {
    mockQueryService = mock(QueryService);

    when(mockQueryService.GetGamesList(anything(), anything())).thenReturn(Observable.from([testGames]));

    TestBed.configureTestingModule({
      declarations: [ GameListComponent, GameStubComponent ],
      imports: [
        MatProgressSpinnerModule,
        InfiniteScrollModule
      ],
      providers: [
        { provide: QueryService, useValue: instance(mockQueryService) }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
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

    it('GamesToLoad should be 10', () => {
      async(() => {
        expect(component.GamesToLoad).toBe(10);
      });
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

    it('GamesToLoad should be 20', () => {
      async(() => {
        expect(component.GamesToLoad).toBe(20);
      });
    });
  });
});
