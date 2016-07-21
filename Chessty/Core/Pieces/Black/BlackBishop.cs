namespace Chessty.Pieces.Black
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackBishop : Bishop
    {
        public BlackBishop(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackBishop Create(Square initialSquare)
        {
            return new BlackBishop(initialSquare);
        }

        public override string ToString()
        {
            return "BlackBishop";
        }
    }
}
