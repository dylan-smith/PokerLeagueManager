import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

import { IPokerConfig } from './IPokerConfig';


export function configFactory(): IPokerConfig {
  return (<IPokerConfig>(<any>window).pokerConfig);
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatExpansionModule
  ],
  providers: [
    //{ provide: 'QUERY_URL', useValue: 'http://queries.pokerleaguemanager.net'}
    { provide: 'POKER_CONFIG', useFactory: configFactory},
    //{ provide: 'QUERY_URL', useValue: (<any>window).pokerConfig.queryServiceUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
