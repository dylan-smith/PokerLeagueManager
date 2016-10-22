declare module app {
    interface IGetGamePlayersDto {
        GameId: string;
        PlayerName: string;
        Placing: number;
        Winnings: number;
        PayIn: number;
    }
}
