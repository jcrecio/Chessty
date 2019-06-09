namespace Chessty.Pieces.Black
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class BlackBishop : Bishop
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public BlackBishop(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackBishop Create(Square initialSquare)
        {
            return new BlackBishop(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "BlackBishop";
        }
    }
}
