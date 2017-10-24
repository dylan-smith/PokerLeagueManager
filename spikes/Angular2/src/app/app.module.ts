import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { GameListComponent } from './game-list/game-list.component';
import { GameComponent } from './game/game.component';
import { QueryService } from './query.service';
import { HttpClientModule } from '@angular/common/http';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MediaCheckService } from './media-check.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule } from '@angular/material';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    GameComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      { path: '', component: GameListComponent},
      { path: 'Home', component: GameListComponent},
      { path: 'Games', component: GameListComponent },
      { path: 'Stats', component: GameListComponent }, 
      { path: 'POTY', component: GameListComponent }, 
      { path: 'Media', component: GameListComponent }, 
      { path: 'Rules', component: GameListComponent }, 
      { path: '**', component: GameListComponent }
  ]),
    HttpClientModule,
    InfiniteScrollModule,
    BrowserAnimationsModule,
    MatButtonModule, 
    MatCheckboxModule
  ],
  providers: [ 
    QueryService,
    { provide: 'QUERY_URL', useValue: 'http://queries.pokerleaguemanager.net'},
    MediaCheckService
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }