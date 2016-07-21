namespace Chessty.Pieces
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Abstract;
    using Chessty.Contracts;
    using System.Collections.Generic;

    using Chessty.Structure;

    public class Bishop : Piece, INotPawnOrKingPiece
    {
        static readonly List<Move> ListMoves = new List<Move>() {
                new Move(1, 1),
                new Move(2, 2),
                new Move(3, 3),
                new Move(4, 4),
                new Move(5, 5),
                new Move(6, 6),
                new Move(7, 7),
                new Move(-1, 1),
                new Move(-2, 2),
                new Move(-3, 3),
                new Move(-4, 4),
                new Move(-5, 5),
                new Move(-6, 6),
                new Move(-7, 7),
                new Move(1, -1),
                new Move(2, -2),
                new Move(3, -3),
                new Move(4, -4),
                new Move(5, -5),
                new Move(6, -6),
                new Move(7, -7),
                new Move(-1, -1),
                new Move(-2, -2),
                new Move(-3, -3),
                new Move(-4, -4),
                new Move(-5, -5),
                new Move(-6, -6),
                new Move(-7, -7),
            };

        public Bishop(PieceColor color, Square initialSquare)
            : base(color, PieceValue.Bishop, initialSquare)
        {
            Developed = false;
        }

        public override void OnAfterMove()
        {
            Developed = true;
        }

        public override void Undo(params bool[] previousValues)
        {
            Developed = previousValues[0];
        }

        public override List<Move> GetMoves()
        {
            return ListMoves;
        }

        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.Bishop;
            }
        }
    }
}
