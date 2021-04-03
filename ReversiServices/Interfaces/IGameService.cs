using ReversiData.Models;
using System.Collections.Generic;

namespace ReversiServices.Interfaces
{
    public interface IGameService
    {
        Game GetDefaultGame();

        void OnMove(ref Game game, ref char[,] gameBoard, ref List<int> legalMoves, ref string message, int xIndex, int yIndex);

        bool IsLegal(char[,] board, int x, int y, char c);

        char[,] MakeMove(char[,] board, int x, int y, char c);

        string UpdateMoves(string old, int x, int y, char c);

        char SwitchTurns(char c);

        List<int> GetAllLegalMoves(char[,] board, char c);

        string GetPlayer(Game g);

        string GetWinner(char[,] board, Game g);
    }
}
