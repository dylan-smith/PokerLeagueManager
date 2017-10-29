import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

export function queryServiceUrlFactory(): string {
  return (<any>window).pokerConfig.queryServiceUrl;
  //return window;
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
    { provide: 'QUERY_URL', useFactory: queryServiceUrlFactory},
    //{ provide: 'QUERY_URL', useValue: (<any>window).pokerConfig.queryServiceUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
