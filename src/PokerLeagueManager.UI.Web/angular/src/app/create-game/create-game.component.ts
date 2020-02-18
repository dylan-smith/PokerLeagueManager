import { Component, OnInit } from '@angular/core';
import { CommandService } from '../command.service';
import { QueryService, IGetPlayersDto } from '../query.service';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { v4 as uuid } from 'uuid';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmCreatePlayerDialogComponent } from '../confirm-create-player-dialog/confirm-create-player-dialog.component';

@Component({
  selector: 'poker-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {
  constructor(private commandService: CommandService, private queryService: QueryService, public dialog: MatDialog) {
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
  NewPlayer: string = "";
  GameDateSet: boolean = true;
  PlayersLoaded: boolean = false;

  public showAddPlayer(): boolean {
    return this.GameDateSet && this.PlayersLoaded;
  }

  ngOnInit() {
    
  }

  public addPlayer(): void {
    let matchPlayer = this.AllPlayers.find(p => p.PlayerName.toLowerCase() == this.NewPlayer.toLowerCase());

    if (matchPlayer)
    {
      this.Players.push(matchPlayer);
      this.NewPlayer = "";
      this.filterPlayers();
    } else {
      const dialogRef = this.dialog.open(ConfirmCreatePlayerDialogComponent, { width: '300px', data: this.NewPlayer });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          let p: IGetPlayersDto = { PlayerId: null, PlayerName: this.NewPlayer, GamesPlayed: 0 };

          this.Players.push(p);
          this.NewPlayer = "";
          this.filterPlayers();
        }
      });
    }
  }

  public AddPlayerButtonVisible(): boolean {
    return this.NewPlayer.trim().length > 0;
  }

  public filterPlayers(): void {
    this.AutoCompletePlayers = this.filter(this.AllPlayers);
  }

  public filter(players: IGetPlayersDto[]): IGetPlayersDto[] {
    return players.filter(player => player.PlayerName.toLowerCase().includes(this.NewPlayer.toLowerCase()) && 
                                    !this.Players.includes(player));
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
}