namespace Chessty.Pieces.Black
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class BlackKing : King
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public BlackKing(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackKing Create(Square initialSquare)
        {
            return new BlackKing(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "BlackKing";
        }

    }
}
