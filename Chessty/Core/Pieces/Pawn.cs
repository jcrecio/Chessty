namespace Chessty.Pieces
{
    using Chessty.Abstract;
    using Chessty.Enumeration;
    using System.Linq;

    using Chessty.Structure;

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

        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.Pawn;
            }
        }
    }
}
