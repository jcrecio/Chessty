namespace Graphical.Board
{
    public class SquareUI
    {
        public static int Empty = -1;

        private static SquareUI emptyInstance;

        public static SquareUI GetEmptyInstance()
        {
            if (emptyInstance == null)
            {
                emptyInstance = new SquareUI
                {
                    Column = Empty,
                    Row = Empty,
                    HasPiece = false
                };
            }

            return emptyInstance;
        }

        public bool HasPiece { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
