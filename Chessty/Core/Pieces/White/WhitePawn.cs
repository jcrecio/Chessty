namespace Chessty.Pieces.White
{
    using System.Collections.Generic;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Structure;

    public class WhitePawn : Pawn
    {
        private static readonly List<Move> Moves = new List<Move> {
                new PawnMove(PawnMoveType.EatLeft, -1, 1),
                new PawnMove(PawnMoveType.AdvanceOne, 0, 1),
                new PawnMove(PawnMoveType.EatRight, 1, 1),
                new PawnMove(PawnMoveType.AdvanceTwo, 0, 2),
                new PawnMove(PawnMoveType.InPassantLeft, -1, 1),
                new PawnMove(PawnMoveType.InPassantRight, 1, 1)
            };

    public WhitePawn(Square square) : base(Enumeration.PieceColor.White, square) { }

        public override List<Move> GetMoves()
        {
            return Moves;
        }

        public static WhitePawn Create(Square initialSquare)
        {
            return new WhitePawn(initialSquare);
        }

        public override string ToString()
        {
            return "WhitePawn";
        }
    }
}
