namespace Chessty.Pieces.Black
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class BlackRock : Rock
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public BlackRock(Square square) : base(PieceColor.Black, square) { }

        public static BlackRock Create(Square initialSquare)
        {
            return new BlackRock(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "BlackRock";
        }
    }
}
