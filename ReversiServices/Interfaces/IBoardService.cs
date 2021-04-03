using ReversiData.Models;

namespace ReversiServices.Interfaces
{
    public interface IBoardService
    {
        char[,] FromBoardStringToArray(string boardString);

        string FromArrayToBoardString(char[,] board);

        string GetCssClassName(char c);
    }
}
