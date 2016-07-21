namespace Chessty.Pieces.Black
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackKing : King
    {
        public BlackKing(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackKing Create(Square initialSquare)
        {
            return new BlackKing(initialSquare);
        }

        public override string ToString()
        {
            return "BlackKing";
        }

    }
}
