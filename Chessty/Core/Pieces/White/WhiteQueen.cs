namespace Chessty.Pieces.White
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class WhiteQueen : Queen
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public WhiteQueen(Square square) : base(PieceColor.White, square) { }

        public static WhiteQueen Create(Square initialSquare)
        {
            return new WhiteQueen(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "WhiteQueen";
        }
    }
}
