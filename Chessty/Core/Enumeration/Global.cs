namespace Chessty.Enumeration
{
    public static class BoardSize
    {
        public static int Rows = 8;
        public static int Columns = 8;
        public static int MaxRowIndex = 7;
        public static int MaxColumnIndex = 7;

    }

    public enum PieceColor
    {
        White, Black
    }
    public enum Direction
    {
        North = 0, South = 4, West = 6, East = 2, Northeast = 1, Northwest = 7, Southest = 3, Southwest = 5
    }
    public enum KnightDirection
    {
        NorthRigth = 0, NorthVeryRight = 1, SouthVeryRight = 2, SouthRight = 3, SouthLeft = 4, SouthVeryLeft = 5, NorthVeryLeft = 6, NorthLeft = 7
    }
    public enum PieceValue
    {
        Pawn = 100, Knight = 320, Bishop = 325, Rock = 525, Queen = 975, King = 20000
    }
    public enum PawnMoveType
    {
        AdvanceOne, AdvanceTwo, EatLeft, EatRight, InPassantLeft, InPassantRight
    }
    public enum NodeType
    {
        Exact, UpperBound, LowerBound
    }

    public enum GamePhase
    {
        Opening = 0,
        MiddleGame = 1,
        EndGame = 2
    }
}
