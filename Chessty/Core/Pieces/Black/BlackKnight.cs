namespace Chessty.Pieces.Black
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class BlackKnight : Knight
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public BlackKnight(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }       
        
        public static BlackKnight Create(Square initialSquare)
        {
            return new BlackKnight(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "BlackKnight";
        }
    }
}
