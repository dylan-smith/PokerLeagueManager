import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { GameListComponent } from './game-list/game-list.component';
import { GameComponent } from './game/game.component';
import { QueryService } from './query.service';
import { HttpClientModule } from '@angular/common/http';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    GameComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: '', component: GameListComponent},
      { path: 'Games', component: GameListComponent },
      { path: 'Stats', component: GameListComponent }, 
      { path: 'POTY', component: GameListComponent }, 
      { path: 'Videos', component: GameListComponent }, 
      { path: 'Pictures', component: GameListComponent }, 
      { path: 'Rules', component: GameListComponent }, 
      { path: '**', component: GameListComponent }, 
  ]),
    HttpClientModule,
    InfiniteScrollModule
  ],
  providers: [ 
    QueryService,
    { provide: 'QUERY_URL', useValue: 'http://queries.pokerleaguemanager.net'}
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }