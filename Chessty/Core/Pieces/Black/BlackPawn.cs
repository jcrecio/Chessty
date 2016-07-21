namespace Chessty.Pieces.Black
{
    using Chessty.Movement;
    using System.Collections.Generic;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class BlackPawn : Pawn
    {
        private static readonly List<Move> Moves = new List<Move> {
                new PawnMove(PawnMoveType.EatLeft, -1, -1),
                new PawnMove(PawnMoveType.EatRight, 1, -1),
                new PawnMove(PawnMoveType.AdvanceTwo, 0, -2),
                new PawnMove(PawnMoveType.AdvanceOne, 0, -1),
                new PawnMove(PawnMoveType.InPassantLeft, -1, -1),
                new PawnMove(PawnMoveType.InPassantRight, 1, -1)
            };

        public BlackPawn(Square square) : base(Chessty.Enumeration.PieceColor.Black, square) { }

        public override List<Move> GetMoves()
        {
            return Moves;
        }

        public override string ToString()
        {
            return "BlackPawn";
        }

        public static BlackPawn Create(Square initialSquare)
        {
            return new BlackPawn(initialSquare);
        }
    }
}
