using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi
{
    public static class GlobalConstants
    {
        public const char BlackPiece = 'B';

        public const char WhitePiece = 'W';

        public const char EmptySpace = 'O';

        //BoardString format: Space = B/W/O depending on type 
        //"{Space}{Space}...{Space}+{Space}{Space}...{Space}+...."
        public const string DefaultBoardString = "OOOOOOOO+OOOOOOOO+OOOOOOOO+OOOBWOOO+OOOWBOOO+OOOOOOOO+OOOOOOOO+OOOOOOOO";
    }
}
