namespace Chessty.Pieces.Black
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackRock : Rock
    {
        public BlackRock(Square square) : base(PieceColor.Black, square) { }

        public static BlackRock Create(Square initialSquare)
        {
            return new BlackRock(initialSquare);
        }

        public override string ToString()
        {
            return "BlackRock";
        }
    }
}
