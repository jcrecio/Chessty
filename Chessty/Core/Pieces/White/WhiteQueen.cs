namespace Chessty.Pieces.White
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class WhiteQueen : Queen
    {
        public WhiteQueen(Square square) : base(PieceColor.White, square) { }

        public static WhiteQueen Create(Square initialSquare)
        {
            return new WhiteQueen(initialSquare);
        }
        public override string ToString()
        {
            return "WhiteQueen";
        }
    }
}
