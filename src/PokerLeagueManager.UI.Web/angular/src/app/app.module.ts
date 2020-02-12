import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';

import { MomentModule } from 'angular2-moment';
import { ApplicationInsightsModule, AppInsightsService } from '@markpieszak/ng-application-insights';

import { AppComponent } from './app.component';
import { GameListComponent } from './game-list/game-list.component';
import { GameComponent } from './game/game.component';
import { CreateGameComponent } from './create-game/create-game.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { QueryService } from './query.service';
import { CommandService } from './command.service';
import { InfiniteScrollModule } from './ngx-infinite-scroll/ngx-infinite-scroll';
import { IPokerConfig } from './IPokerConfig';

// doing it this way because the old (better) way with useValue instead of useFactory
// breaks when you do ng build --prod
export function configFactory(): IPokerConfig {
  return (<IPokerConfig>globalThis.pokerConfig);
}

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    GameComponent,
    CreateGameComponent,
    NavbarComponent,
    SideNavComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: GameListComponent},
      { path: 'Home', component: GameListComponent},
      { path: 'Games', component: GameListComponent },
      { path: 'Stats', component: GameListComponent },
      { path: 'POTY', component: GameListComponent },
      { path: 'Media', component: GameListComponent },
      { path: 'Rules', component: GameListComponent },
      { path: 'CreateGame', component: CreateGameComponent },
      { path: '**', component: GameListComponent }
  ]),
    HttpClientModule,
    InfiniteScrollModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
    MomentModule,
    ApplicationInsightsModule.forRoot({
      instrumentationKey: globalThis.pokerConfig.appInsightsKey
    })
  ],
  providers: [
    QueryService,
    CommandService,
    { provide: 'POKER_CONFIG', useFactory: configFactory},
    AppInsightsService
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
