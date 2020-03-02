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
      this.AutoCompletePlayers = this.AllPlayers;
      this.PlayersLoaded = true;
    });
  }

  GameDate: Date;
  GameId: string;
  Players: IGetPlayersDto[] = [];
  AllPlayers: IGetPlayersDto[];
  AutoCompletePlayers: IGetPlayersDto[];
  NewPlayer: string = "";
  GameDateSet: boolean = false;
  PlayersLoaded: boolean = false;

  public showAddPlayer(): boolean {
    return this.GameDateSet && this.PlayersLoaded;
  }

  ngOnInit() {
    
  }

  private AddPlayerToGame(player: IGetPlayersDto): void {
    // what if this errors?
    this.commandService.AddPlayerToGame(player.PlayerId, this.GameId).subscribe();
    this.Players.push(player);
    this.NewPlayer = "";
    this.filterPlayers();
    // refresh the game/player data
  }

  public AddPlayerClicked(): void {
    let matchingPlayer = this.AllPlayers.find(p => p.PlayerName.toLowerCase() == this.NewPlayer.toLowerCase());

    if (matchingPlayer)
    {
      this.AddPlayerToGame(matchingPlayer);
    } else {
      const dialogRef = this.dialog.open(ConfirmCreatePlayerDialogComponent, { width: '300px', data: this.NewPlayer });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          let p: IGetPlayersDto = { PlayerId: uuid(), PlayerName: this.NewPlayer, GamesPlayed: 0 };

          // What if this errors?
          this.commandService.CreatePlayer(p.PlayerId, p.PlayerName).subscribe();
          this.AddPlayerToGame(p);
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