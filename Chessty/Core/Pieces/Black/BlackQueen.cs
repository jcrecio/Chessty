namespace Chessty.Pieces.Black
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class BlackQueen : Queen
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public BlackQueen(Square initialSquare) : base(PieceColor.Black, initialSquare)
        {
        }

        public static BlackQueen Create(Square initialSquare)
        {
            return new BlackQueen(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "BlackQueen";
        }
    }
}
