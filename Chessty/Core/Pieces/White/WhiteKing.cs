namespace Chessty.Pieces.White
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class WhiteKing : King
    {
        public WhiteKing(Square initialSquare) : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteKing Create(Square initialSquare)
        {
            return new WhiteKing(initialSquare);
        }

        public override string ToString()
        {
            return "WhiteKing";
        }
    }
}
