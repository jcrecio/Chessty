namespace Chessty.Pieces
{
    using Chessty.Abstract;
    using Chessty.Enumeration;
    using System.Linq;

    using Chessty.Structure;
    using Movement;
    using White;
    using Black;
    using System;

    public abstract class Pawn : Piece
    {
        public bool IsPassantLeft { get; private set; }
        public bool IsPassantRight { get; private set; } //true = ready
        public bool AdvanceTwo { get; private set; }

        protected Pawn(PieceColor color, Square initialSquare) : base(color, PieceValue.Pawn, initialSquare)
        {
            this.IsPassantLeft = false;
            this.IsPassantRight = false;
            this.AdvanceTwo = true; // TODO IN PASSANT
        }

        public override void OnAfterMove()
        {
            this.IsPassantLeft = false;
            this.IsPassantRight = false;
            this.AdvanceTwo = false;
        }

        public override void Undo(params bool[] previousValues)
        {
            if (previousValues.Count() != 3) return;

            this.IsPassantLeft = previousValues[0];
            this.IsPassantRight = previousValues[1];
            this.AdvanceTwo = previousValues[2];
        }

        public override int GetMoveByPriority(MoveDefinition play, Square squareTo, Func<MoveDefinition, Square, bool> precondition = null)
        {
            var pawnMove = play.Move as PawnMove;
            var colorEqualsWhite = play.Square.CurrentPiece.Color == PieceColor.White;

            Pawn pawnFrom;
            if (colorEqualsWhite)
            {
                pawnFrom = play.Square.CurrentPiece as WhitePawn;
            }
            else
            {
                pawnFrom = play.Square.CurrentPiece as BlackPawn;
            }

            var pawnType = pawnMove.Type;
            var pieceTo = squareTo.CurrentPiece;

            if ((pawnType == PawnMoveType.EatLeft || pawnType == PawnMoveType.EatRight)
                && (pieceTo != null && pieceTo.Color != play.Square.CurrentPiece.Color))
            {
                return 3;//+ pieceTo.Value - PieceValue.Pawn;
            }

            if ((pawnType == PawnMoveType.InPassantLeft && pawnFrom.IsPassantLeft) && pieceTo == null)
            {
                return 0;
            }

            if ((pawnType == PawnMoveType.InPassantRight && pawnFrom.IsPassantRight) && pieceTo == null)
            {
                return 0;
            }

            if (pawnType == PawnMoveType.AdvanceOne && pieceTo == null && precondition != null && precondition(play, squareTo))
            {
                return 1;
            }

            if ((pawnType == PawnMoveType.AdvanceTwo && pawnFrom.AdvanceTwo) 
                && play.Board.GetSquare(play.Square.Column, colorEqualsWhite ? 2 : 5).CurrentPiece == null
                    && pieceTo == null)
            {
                return 2;
            }

            return 0;
        }


        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.Pawn;
            }
        }
    }
}
