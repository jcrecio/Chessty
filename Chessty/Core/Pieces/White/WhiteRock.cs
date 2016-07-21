namespace Chessty.Pieces.White
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class WhiteRock: Rock
    {
        public WhiteRock(Square initialSquare)
            : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteRock Create(Square initialSquare)
        {
            return new WhiteRock(initialSquare);
        }
        public override string ToString()
        {
            return "WhiteRock";
        }
    }
}
