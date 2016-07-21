namespace Chessty
{
    using System;

    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Pieces;
    using Chessty.Pieces.White;
    using Chessty.Abstract;
    using Chessty.Structure;

    public class Operator: IEquatable<Operator>
    {
        public Chessty.Movement.Move Move { get; set; }
        public Square Square { get; set; }
        public int Type { get; set; }

        public Operator(Chessty.Movement.Move move, Square square)
        {
            this.Move = move;
            this.Square = square;
        }

        public Operator(Move move, Square square, int type)
        {
            this.Move = move;
            this.Square = square;
            this.Type = type;
        }

        public Square Operate(Board board, out Piece pieceToReset)
        {
            var targetSquare = board.GetSquare(this.Square.Column + this.Move.Column, this.Square.Row + this.Move.Row);

            pieceToReset = targetSquare.CurrentPiece;

            this.Square.MovePieceToSquare(targetSquare);
            if (targetSquare.CurrentPiece is King)
            {
                this.SetKing(board, targetSquare);
            }

            return targetSquare;
        }

        public void Undo(Board board, Square previousSquare, Square targetSquare, Piece pieceToReset, bool[] previousValues = null)
        {
            var currentPiece = previousSquare.CurrentPiece;

            targetSquare.CurrentPiece = currentPiece;
            if (currentPiece is King)
            {
                this.SetKing(board, targetSquare);
            }

            previousSquare.CurrentPiece = pieceToReset;
            if (currentPiece != null && previousValues != null)
            {
                currentPiece.Undo(previousValues);
            }
        }

        private void SetKing(Board board, Square targetSquare)
        {
            if (targetSquare.CurrentPiece.Color.Equals(PieceColor.White))
            {
                board.WhiteKingPosition = targetSquare;
            }
            else
            {
                board.BlackKingPosition = targetSquare;
            }
        }

        public bool Equals(Operator other)
        {
            return Square.Column == other.Square.Column && Square.Row == other.Square.Row &&
                   Move.Column == other.Move.Column && Move.Row == other.Move.Row;
        }

        public override string ToString()
        {
            return string.Concat(Square.ToString(), "->(", Move.Column, ",", Move.Row, ")");
        }

        public Operator Clone()
        {
            return new Operator(this.Move.Clone(), this.Square.Clone());
        }
    }
}
