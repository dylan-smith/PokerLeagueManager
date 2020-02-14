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
    queryService.GetPlayers().subscribe(players => {
      this.AllPlayers = players;
      this.PlayersLoaded = true;
      this.AutoCompletePlayers = this.AllPlayers;
    });
  }

  GameDate: Date;
  GameId: string;
  Players: IGetPlayersDto[] = [];
  AllPlayers: IGetPlayersDto[];
  AutoCompletePlayers: IGetPlayersDto[];
  NewPlayer: string;
  GameDateSet: boolean = true;
  PlayersLoaded: boolean = false;

  public showAddPlayer(): boolean {
    return this.GameDateSet && this.PlayersLoaded;
  }

  ngOnInit() {
    
  }

  public addPlayer(): void {
    // let matchPlayer: IGetPlayersDto = undefined;
    let matchPlayer = this.AllPlayers.find(p => p.PlayerName.toLowerCase() == this.NewPlayer.toLowerCase());
    //this.AllPlayers.subscribe(p => matchPlayer = p.find(x => x.PlayerName.toLowerCase() == this.NewPlayer.toLowerCase()));

    if (matchPlayer)
    {
      this.Players.push(matchPlayer);
    }
  }

  public filterPlayers(): void {
    //this.AutoCompletePlayers = this.AllPlayers.pipe(map(players => this.filter(players)));
    this.AutoCompletePlayers = this.filter(this.AllPlayers);
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
