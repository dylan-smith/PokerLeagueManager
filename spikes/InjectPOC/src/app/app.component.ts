import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  myUrl : string;

  constructor(@Inject('QUERY_URL') private QUERY_URL: string) {
    this.myUrl = QUERY_URL;
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
