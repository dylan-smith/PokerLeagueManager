import { Component } from '@angular/core';
import { CommandService } from '../command.service'
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { v4 as uuid } from 'uuid';

@Component({
  selector: 'poker-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent {
  constructor(private commandService: CommandService) { }

  GameDate: Date;
  GameId: string;

  public addPlayer(): void {

  }

  public gameDateChanged(event: MatDatepickerInputEvent<Date>) {
    if (!this.GameId) {
      this.GameId = uuid();
      this.commandService.CreateGame(this.GameId, this.GameDate.toDateString()).subscribe();
    } else {
      this.commandService.ChangeGameDate(this.GameId, this.GameDate.toDateString()).subscribe();
    }
  }
}
