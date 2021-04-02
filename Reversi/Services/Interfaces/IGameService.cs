﻿using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Services.Interfaces
{
    public interface IGameService
    {
        Game GetDefaultGame();

        bool IsLegal(char[,] board, int x, int y, char c);

        char[,] MakeMove(char[,] board, int x, int y, char c);

        string UpdateMoves(string old, int x, int y, char c);

        char SwitchTurns(char c);

        bool IsGameEnded(Game g, out string winner);

        List<int> GetAllLegalMoves(char[,] board, char c);
    }
}