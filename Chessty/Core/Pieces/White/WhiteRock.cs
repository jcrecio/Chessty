namespace Chessty.Pieces.White
{
    using Chessty.Enumeration;
    using Chessty.Structure;
    using System;

    public class WhiteRock: Rock
    {
        public static Guid TypeId { get; } = Guid.NewGuid();

        public WhiteRock(Square initialSquare)
            : base(PieceColor.White, initialSquare)
        {
        }

        public static WhiteRock Create(Square initialSquare)
        {
            return new WhiteRock(initialSquare);
        }

        public override Guid GetTypeId()
        {
            return TypeId;
        }

        public override string ToString()
        {
            return "WhiteRock";
        }
    }
}
