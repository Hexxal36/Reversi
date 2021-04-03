using ReversiServices.Interfaces;
using ReversiShared;

namespace ReversiServices
{
    public class BoardService : IBoardService
    {
        public char[,] FromBoardStringToArray(string boardString)
        {
            var rows = boardString.Split('+');

            var rowLength = rows[0].Length;

            var result = new char[rowLength, rowLength];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    result[i, j] = rows[i][j];
                }
            }

            return result;
        }

        public string FromArrayToBoardString(char[,] board)
        {
            var boardSize = board.GetLength(0);

            var result = "";

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    result += board[i, j];
                }

                result += "+";
            }

            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public string GetCssClassName(char c)
        {
            return c switch
            {
                GlobalConstants.BlackPiece => "black",
                GlobalConstants.WhitePiece => "white",
                GlobalConstants.EmptySpace => "empty",
                _ => ""
            };
        }
    }
}
