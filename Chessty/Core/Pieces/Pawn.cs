namespace Chessty.Pieces
{
    using Chessty.Abstract;
    using Chessty.Enumeration;
    using System.Linq;

    using Chessty.Structure;
    using Movement;
    using White;
    using Black;

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

        public int GetMoveByPriority(Move move, Board board, Square squareTo, Piece pieceFrom)
        {
            var pawnMove = move as PawnMove;
            var colorEqualsWhite = pieceFrom.Color == PieceColor.White;

            Pawn pawnFrom;
            if (colorEqualsWhite)
            {
                pawnFrom = pieceFrom as WhitePawn;
            }
            else
            {
                pawnFrom = pieceFrom as BlackPawn;
            }

            var pawnType = pawnMove.Type;
            var pieceTo = squareTo.CurrentPiece;

            if ((pawnType == PawnMoveType.EatLeft || pawnType == PawnMoveType.EatRight)
                && (pieceTo != null && pieceTo.Color != pieceFrom.Color))
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

            if (pawnType == PawnMoveType.AdvanceOne && pieceTo == null)

            {
                return 1;
            }

            if ((pawnType == PawnMoveType.AdvanceTwo && pawnFrom.AdvanceTwo) && board.GetSquare(squareTo.Column, colorEqualsWhite ? 2 : 5).CurrentPiece == null
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
