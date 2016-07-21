namespace Chessty.Pieces.Black
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackQueen : Queen
    {
        public BlackQueen(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackQueen Create(Square initialSquare)
        {
            return new BlackQueen(initialSquare);
        }

        public override string ToString()
        {
            return "BlackQueen";
        }
    }
}
