import { Component, Inject } from '@angular/core';
import { IPokerConfig } from './IPokerConfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  myUrl : string;

  constructor(@Inject('POKER_CONFIG') private POKER_CONFIG: IPokerConfig) {
    this.myUrl = POKER_CONFIG.queryServiceUrl;
    //this.myUrl = (<any>injectedWindow).pokerConfig.queryServiceUrl;
    //this.myUrl = injectedWindow;
  }

  public GameClicked(): void {
    console.log("GameClicked");
  }

  GameClosed(): void {
    console.log("GameClosed");
  }
}
