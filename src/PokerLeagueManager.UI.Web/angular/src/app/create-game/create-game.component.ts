import { Component, OnInit } from '@angular/core';
import { CommandService } from '../command.service';
import { QueryService, IGetPlayersDto } from '../query.service';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { v4 as uuid } from 'uuid';
import { Observable } from 'rxjs/Observable';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'poker-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {
  constructor(private commandService: CommandService, private queryService: QueryService) {
    this.AllPlayers = queryService.GetPlayers();
  }

  GameDate: Date;
  GameId: string;
  Players: IGetPlayersDto[];
  AllPlayers: Observable<IGetPlayersDto[]>;
  AutoCompletePlayers: Observable<IGetPlayersDto[]>;
  NewPlayer: string;
  GameDateSet: boolean = false;

  public showAddPlayerButton(): boolean {
    return this.GameDateSet;
  }

  ngOnInit() {
    this.AutoCompletePlayers = this.AllPlayers;
  }

  public addPlayer(): void {
    
  }

  public filterPlayers(): void {
    this.AutoCompletePlayers = this.AllPlayers.pipe(map(players => this.filter(players)));
  }

  public filter(players: IGetPlayersDto[]): IGetPlayersDto[] {
    return players.filter(player => player.PlayerName.toLowerCase().includes(this.NewPlayer.toLowerCase()));
  }

  public gameDateChanged(event: MatDatepickerInputEvent<Date>) {
    if (!this.GameId) {
      this.GameId = uuid();
      this.commandService.CreateGame(this.GameId, this.GameDate.toDateString()).subscribe();
    } else {
      this.commandService.ChangeGameDate(this.GameId, this.GameDate.toDateString()).subscribe();
    }

    this.GameDateSet = true;
  }

  options = { autoHide: false, scrollbarMinSize: 100 };
}
