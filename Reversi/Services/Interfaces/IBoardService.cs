using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Services.Interfaces
{
    public interface IBoardService
    {
        char[,] FromBoardStringToArray(string boardString);

        string FromArrayToBoardString(char[,] board);

        string GetCssClassName(char c);
    }
}
