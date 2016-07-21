namespace Chessty.Structure
{
    using System;
    using System.Collections.Generic;
    using Chessty.Abstract;
    using Chessty.Contracts;
    using Chessty.Movement;

    public class Square : Vector, IVectorize
    {
        public Piece CurrentPiece { get; set; }

        public Guid Identifier { get; private set; }

        public Square()
            :this(0,0)
        {
        }

        public Square(int column, int row)
        {
            this.Row = row;
            this.Column = column;

            this.Identifier = Guid.NewGuid();
        }

        public void MovePieceToSquare(Square square)
        {
            var currentPiece = this.CurrentPiece;

            square.CurrentPiece = currentPiece;
            if (square.CurrentPiece != null)
            {
                square.CurrentPiece.OnAfterMove();
            }

            this.CurrentPiece = null;
        }

        public string GetHashSquare()
        {
            return this.CurrentPiece != null ? this.CurrentPiece.GetHashPiece() : "0";
        }

        public override string ToString()
        {
            if (this.CurrentPiece != null)
            {
                return string.Format("({0},{1})->{2}", this.Column, this.Row, this.CurrentPiece);
            }

            return "____|";
        }

        public Vector GetUnitarianVector()
        {
            return new Square(this.Column == 0 ? 0 : Math.Abs(this.Column) / this.Column, this.Row == 0 ? 0 : Math.Abs(this.Row) / this.Row);
        }

        public Square Clone()
        {
            return new Square(this.Column, this.Row);
        }
    }
}