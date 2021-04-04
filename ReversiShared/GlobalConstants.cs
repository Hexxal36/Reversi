namespace ReversiShared
{
    public static class GlobalConstants
    {
        public const char BlackPiece = 'B';

        public const char WhitePiece = 'W';

        public const char EmptySpace = 'O';

        //BoardString format: Space = B/W/O depending on type 
        //"{Space}{Space}...{Space}+{Space}{Space}...{Space}+...."
        public const string DefaultBoardString = "OOOOOOOO+OOOOOOOO+OOOOOOOO+OOOBWOOO+OOOWBOOO+OOOOOOOO+OOOOOOOO+OOOOOOOO";

        public const string DefaultPlayerOne = "-----|player-one|-----";
        public const string DefaultPlayerTwo = "-----|player-two|-----";

        public const string GuestMessage = "You are a guest!";
    }
}
