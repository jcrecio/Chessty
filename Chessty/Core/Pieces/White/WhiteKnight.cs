namespace Chessty.Pieces.White
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class WhiteKnight : Knight
    {
        public WhiteKnight(Square initialSquare) : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteKnight Create(Square initialSquare)
        {
            return new WhiteKnight(initialSquare);
        }

        public override string ToString()
        {
            return "WhiteKnight";
        }
    }
}
