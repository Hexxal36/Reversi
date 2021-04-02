using Reversi.Models;
using Reversi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Services
{
    public class GameService : IGameService
    {
        private readonly IBoardService _boardService;

        public GameService(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public Game GetDefaultGame()
            => new Game()
            {
                Moves = "",
                BoardString = GlobalConstants.DefaultBoardString,
                PlayerOne = "def",
                PlayerTwo = "def",
                OnTurn = GlobalConstants.BlackPiece
            };


        public bool IsLegal(char[,] board, int x, int y, char c)
        {
            if (board[x, y] != GlobalConstants.EmptySpace)
            {
                return false;
            }

            //check each direction


            //x+
            if (_CheckDirection(board, x, y, c, 1, 0))
            {
                return true;
            }

            //y+
            if (_CheckDirection(board, x, y, c, 0, 1))
            {
                return true;
            }

            //x-
            if (_CheckDirection(board, x, y, c, -1, 0))
            {
                return true;
            }

            //y-
            if (_CheckDirection(board, x, y, c, 0, -1))
            {
                return true;
            }

            //x+y+
            if (_CheckDirection(board, x, y, c, 1, 1))
            {
                return true;
            }

            //x+y-
            if (_CheckDirection(board, x, y, c, 1, -1))
            {
                return true;
            }

            //x-y+
            if (_CheckDirection(board, x, y, c, -1, 1))
            {
                return true;
            }

            //x-y-
            if (_CheckDirection(board, x, y, c, -1, -1))
            {
                return true;
            }

            return false;
        }

        public char[,] MakeMove(char[,] board, int x, int y, char c)
        {
            board[x, y] = c;

            //x+
            board = _FlipInDirection(board, x, y, c, 1, 0);
            //x-
            board = _FlipInDirection(board, x, y, c, -1, 0);
            //y+
            board = _FlipInDirection(board, x, y, c, 0, 1);
            //y-
            board = _FlipInDirection(board, x, y, c, 0, -1);
            //x+y+
            board = _FlipInDirection(board, x, y, c, 1, 1);
            //x+y-
            board = _FlipInDirection(board, x, y, c, 1, -1);
            //x-y+
            board = _FlipInDirection(board, x, y, c, -1, 1);
            //x-y-
            board = _FlipInDirection(board, x, y, c, -1, -1);

            return board;
        }

        public bool IsGameEnded(Game g, out string winner)
        {
            winner = "";

            return false;
        }

        public char SwitchTurns(char c)
            => c switch
            {
                GlobalConstants.BlackPiece => GlobalConstants.WhitePiece,
                GlobalConstants.WhitePiece => GlobalConstants.BlackPiece,
                _ => GlobalConstants.BlackPiece
            };

        public string UpdateMoves(string oldMoves, int x, int y, char c)
            => $"{oldMoves};{c}{x}{y}";

        public List<int> GetAllLegalMoves(char[,] board, char c)
        {
            var boardSize = board.GetLength(0);

            var result = new List<int>();

            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (IsLegal(board, x, y, c))
                    {
                        result.Add(x * 10 + y);
                    }
                }
            }

            return result;
        }

        //x/y option:
        // +1 increment
        // -1 decrement
        // 0 ignore
        private bool _CheckDirection(char[,] board, int x, int y, char c, int xOption, int yOption)
        {
            try
            {
                var oponent = SwitchTurns(c);

                var xIndex = x + xOption;
                var yIndex = y + yOption;

                var hasSeenOponent = false;

                while (board[xIndex, yIndex] == oponent)
                {
                    hasSeenOponent = true;

                    xIndex += xOption;
                    yIndex += yOption;
                }

                if (board[xIndex, yIndex] == c && hasSeenOponent)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        //x/y option:
        // +1 increment
        // -1 decrement
        // 0 ignore
        private char[,] _FlipInDirection(char[,] board, int x, int y, char c, int xOption, int yOption)
        {
            try
            {
                var oponent = SwitchTurns(c);

                var xIndex = x + xOption;
                var yIndex = y + yOption;

                while (board[xIndex, yIndex] == oponent)
                {
                    xIndex += xOption;
                    yIndex += yOption;
                }

                var hasSeenSelf = board[xIndex, yIndex] == c;

                if (hasSeenSelf)
                {
                    xIndex -= xOption;
                    yIndex -= yOption;

                    while (board[xIndex, yIndex] == oponent)
                    {
                        board[xIndex, yIndex] = c;

                        xIndex -= xOption;
                        yIndex -= yOption;
                    }
                }

            }
            catch
            {
            }

            return board;
        }
    }
}
