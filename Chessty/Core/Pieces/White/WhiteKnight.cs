namespace Chessty.Pieces.White
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class WhiteKnight : Knight
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public WhiteKnight(Square initialSquare) : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteKnight Create(Square initialSquare)
        {
            return new WhiteKnight(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "WhiteKnight";
        }
    }
}
