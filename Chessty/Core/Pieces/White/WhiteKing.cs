namespace Chessty.Pieces.White
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class WhiteKing : King
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public WhiteKing(Square initialSquare) : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteKing Create(Square initialSquare)
        {
            return new WhiteKing(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "WhiteKing";
        }
    }
}
