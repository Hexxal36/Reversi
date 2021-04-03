using ReversiData.Models;
using ReversiServices.Interfaces;
using ReversiShared;
using System.Collections.Generic;
using System.Linq;

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
                PlayerOne = GlobalConstants.DefaultPlayerOne,
                PlayerTwo = GlobalConstants.DefaultPlayerTwo,
                OnTurn = GlobalConstants.BlackPiece
            };

        public bool IsOnHisTurn(Game g, string user)
        {
            if (g.IsPlayerOneBlack)
            {
                if (g.OnTurn == GlobalConstants.BlackPiece && user == g.PlayerOne)
                {
                    return true;
                }
                if (g.OnTurn == GlobalConstants.WhitePiece && user == g.PlayerTwo)
                {
                    return true;
                }
            }
            else
            {
                if (g.OnTurn == GlobalConstants.BlackPiece && user == g.PlayerTwo)
                {
                    return true;
                }
                if (g.OnTurn == GlobalConstants.WhitePiece && user == g.PlayerOne)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetPlayerStatus(Game game, string userId)
        {

            if (game.IsPlayerOneBlack && game.PlayerOne == userId)
            {
                return "black";
            }
            if (game.IsPlayerOneBlack && game.PlayerTwo == userId)
            {
                return "white";
            }
            if (!game.IsPlayerOneBlack && game.PlayerOne == userId)
            {
                return "white";
            }
            if (!game.IsPlayerOneBlack && game.PlayerTwo == userId)
            {
                return "black";
            }

            return "a guest";
        }

        public void OnMove(ref Game game, ref char[,] gameBoard, ref List<int> legalMoves, ref string message, int xIndex, int yIndex)
        {
            if (game.Winner is null)
            {
                if (IsLegal(gameBoard, xIndex, yIndex, game.OnTurn))
                {
                    gameBoard = MakeMove(gameBoard, xIndex, yIndex, game.OnTurn);

                    game.BoardString = _boardService.FromArrayToBoardString(gameBoard);

                    game.Moves = UpdateMoves(game.Moves, xIndex, yIndex, game.OnTurn);

                    game.OnTurn = SwitchTurns(game.OnTurn);

                    legalMoves = GetAllLegalMoves(gameBoard, game.OnTurn);

                    if (!legalMoves.Any())
                    {
                        message = $"{GetPlayer(game)} has no legal moves!";
                        game.OnTurn = SwitchTurns(game.OnTurn);

                        legalMoves = GetAllLegalMoves(gameBoard, game.OnTurn);

                        if (!legalMoves.Any())
                        {
                            game.Winner = GetWinner(gameBoard, game);
                            message = $"{game.Winner} wins!";
                        }
                    }

                    message = "";
                }
            }
        }


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

        public string GetPlayer(Game g)
            => g.OnTurn switch
            {
                GlobalConstants.BlackPiece => g.PlayerOne,
                GlobalConstants.WhitePiece => g.PlayerTwo,
                _ => g.PlayerOne
            };

        public string GetWinner(char[,] board, Game g)
        {
            int blackPieces = 0;
            int whitePieces = 0;

            int boardSize = board.GetLength(0);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == GlobalConstants.BlackPiece)
                    {
                        blackPieces++;
                    }
                    else if (board[i, j] == GlobalConstants.WhitePiece)
                    {
                        whitePieces++;
                    }
                }
            }

            return (blackPieces - whitePieces) switch
            {
                > 0 => g.PlayerOne,
                < 0 => g.PlayerTwo,
                _ => "Everybody"
            };
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
