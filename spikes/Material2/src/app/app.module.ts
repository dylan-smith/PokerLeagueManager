import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MatButtonModule, MatExpansionModule, MatSidenavModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GameListComponent } from './game-list/game-list.component';

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent
  ],
  imports: [
    BrowserModule,
    MatSidenavModule,
    BrowserAnimationsModule,
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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
