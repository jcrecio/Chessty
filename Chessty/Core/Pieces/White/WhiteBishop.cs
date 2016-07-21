namespace Chessty.Pieces.White
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class WhiteBishop: Bishop
    {
        public WhiteBishop(Square initialSquare)
            : base(PieceColor.White, initialSquare)
        {
        }
        public static WhiteBishop Create(Square initialSquare)
        {
            return new WhiteBishop(initialSquare);
        }

        public override string ToString()
        {
            return "WhiteBishop";
        }
    }
}
