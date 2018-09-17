namespace Chessty.Movement
{
    using System;
    using Chessty.Enumeration;
    using Chessty.Structure;

    public class MoveDefinition
    {
        private Square moveDefinitionOutput;
        private Move _move;
        private Square _square;

        public Board Board { get; set; }
        public Move Move
        {
            get => _move;
            set
            {
                moveDefinitionOutput = null;
                _move = value;
            }
        }

        public Square Square {
            get => _square;
            set
            {
                moveDefinitionOutput = null;
                _square = value;
            }
        }

        public bool IsInCheck { get; set; }

        public bool CanBeDone()
        {
            var squareToColumn = Square.Column + Move.Column;
            var squareToRow = Square.Row + Move.Row;

            return Board.IsValidSquare(squareToColumn, squareToRow) && Square.PieceCanMoveToSquare(OutputSquare());
        }

        private Square OutputSquare()
        {
            if (moveDefinitionOutput == null)
            {
                var squareToColumn = Square.Column + Move.Column;
                var squareToRow = Square.Row + Move.Row;

                moveDefinitionOutput = Board.GetSquare(squareToColumn, squareToRow);
            }

            return moveDefinitionOutput;
        }

        private bool SquaresRangeIsEmpty(Square squareTo)
        {
            return Board.SquaresRangeEmpty(squareTo.Row, squareTo.Column, Move.GetUnitarian(), Square);
        }

        public int GetMoveByPriority()
        {
            return GetMoveByPriority(OutputSquare(), this.SquaresRangeIsEmpty);
        }

        public virtual int GetMoveByPriority(Square squareTo, Func<Square, bool> precondition = null)
        {
            if (precondition != null && !precondition(squareTo))
            {
                return Globals.MoveCannotBeDone;
            }

            var valuePieceInSquareTo = squareTo.GetPieceValue();

            if (valuePieceInSquareTo == 0)
            {
                return Globals.MoveIsNormal;
            }

            if (Square.GetPieceValue() == PieceValue.Queen)
            {
                return Globals.MoveIsNormal;
            }

            if (valuePieceInSquareTo > Square.GetPieceValue())
            {
                return 2 + valuePieceInSquareTo - Square.GetPieceValue();
            }

            return Globals.MoveIsCapture;
        }
    }
}