namespace Chessty.Pieces.White
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class WhiteBishop: Bishop
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public WhiteBishop(Square initialSquare)
            : base(PieceColor.White, initialSquare)
        {
        }
        public static WhiteBishop Create(Square initialSquare)
        {
            return new WhiteBishop(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "WhiteBishop";
        }
    }
}
