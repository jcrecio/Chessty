namespace Chessty.Pieces.Black
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackKnight : Knight
    {
        public BlackKnight(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }       
        
        public static BlackKnight Create(Square initialSquare)
        {
            return new BlackKnight(initialSquare);
        }

        public override string ToString()
        {
            return "BlackKnight";
        }
    }
}
