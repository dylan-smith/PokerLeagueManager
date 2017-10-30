import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NgZone } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { GameListComponent } from './game-list/game-list.component';
import { GameComponent } from './game/game.component';
import { QueryService } from './query.service';
import { HttpClientModule } from '@angular/common/http';
import { InfiniteScrollModule } from './ngx-infinite-scroll/ngx-infinite-scroll';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatExpansionModule, MatSidenavModule, MatIconModule, MatProgressSpinnerModule, MatTableModule } from '@angular/material';
import { NavbarComponent } from './navbar/navbar.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { MomentModule } from 'angular2-moment';
import { ApplicationInsightsModule, AppInsightsService } from '@markpieszak/ng-application-insights';
import { IPokerConfig } from './IPokerConfig';

// doing it this way because the old (better) way with useValue instead of useFactory
// breaks when you do ng build --prod
export function configFactory(): IPokerConfig {
  return (<IPokerConfig>(<any>window).pokerConfig);
}

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    GameComponent,
    NavbarComponent,
    SideNavComponent
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
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MomentModule,
    ApplicationInsightsModule.forRoot({
      instrumentationKey: (<any>window).pokerConfig.appInsightsKey
    })
  ],
  providers: [
    QueryService,
    { provide: 'POKER_CONFIG', useFactory: configFactory},
    AppInsightsService
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
