import { Component, OnInit } from '@angular/core';
import { CommandService } from '../command.service';
import { QueryService, IGetPlayersDto, IGetGamePlayersDto } from '../query.service';
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
  Players: IGetGamePlayersDto[] = [];
  AllPlayers: IGetPlayersDto[];
  AutoCompletePlayers: IGetPlayersDto[];
  NewPlayer: string = "";
  GameDateSet: boolean = false;
  PlayersLoaded: boolean = false;
  Refreshing: boolean = false;

  public PlayerInputVisible(): boolean {
    return this.GameDateSet && this.PlayersLoaded;
  }

  ngOnInit() {
    
  }

  private AddPlayerToGame(playerId: string, playerName: string): void {
    // what if this errors? await?
    this.commandService.AddPlayerToGame(playerId, this.GameId).subscribe(() => {
      this.refreshData();
    });
    
    let p: IGetGamePlayersDto = {
      GameId: this.GameId,
      PlayerId: playerId,
      PlayerName: playerName,
      PayIn: 0,
      Winnings: 0,
      Placing: 0
    }

    this.Players.push(p);
    this.Players = this.Players.sort((a, b) => a.PlayerName.localeCompare(b.PlayerName));
    this.NewPlayer = "";
    this.filterPlayers();
  }

  private refreshData(): void {
    this.Refreshing = true;

    this.queryService.GetGamePlayers(this.GameId)
      .subscribe(players => {
        this.Players = players.sort((a, b) => a.PlayerName.localeCompare(b.PlayerName));
        this.Refreshing = false;
      });
  }

  public AddPlayerClicked(): void {
    let matchingPlayer = this.AllPlayers.find(p => p.PlayerName.toLowerCase() == this.NewPlayer.toLowerCase());

    if (matchingPlayer)
    {
      this.AddPlayerToGame(matchingPlayer.PlayerId, matchingPlayer.PlayerName);
    } else {
      const dialogRef = this.dialog.open(ConfirmCreatePlayerDialogComponent, { width: '300px', data: this.NewPlayer });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          var newPlayerId = uuid();

          // What if this errors? await?
          this.commandService.CreatePlayer(newPlayerId, this.NewPlayer).subscribe();
          this.AddPlayerToGame(newPlayerId, this.NewPlayer);
        }
      });
    }
  }

  public AddPlayerButtonVisible(): boolean {
    return this.NewPlayer.trim().length > 0 && !this.Refreshing;
  }

  public filterPlayers(): void {
    this.AutoCompletePlayers = this.filter(this.AllPlayers);
  }

  public filter(players: IGetPlayersDto[]): IGetPlayersDto[] {
    return players.filter(player => player.PlayerName.toLowerCase().includes(this.NewPlayer.toLowerCase()) && 
                                    !this.Players.some(p => p.PlayerId == player.PlayerId));
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