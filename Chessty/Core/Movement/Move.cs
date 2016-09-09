namespace Chessty.Movement
{
    using System;
    using Chessty.Contracts;
    using System.Collections.Generic;
    public class Move : Vector, IVectorize, IIsDirectionable
    {
        public int MoveType { get; set; } // 0 normal, 1 pawn, 2 king

        private class MoveCoordinatesContainer
        {
            public int Row { get; set; }
            public int Column { get; set; }
        }

        private static IDictionary<int, MoveCoordinatesContainer> DirectionsTable = new Dictionary<int, MoveCoordinatesContainer>() {
            { 0 , new MoveCoordinatesContainer { Row = 1, Column = 0 } },
            { 1 , new MoveCoordinatesContainer { Row = 1, Column = 1 } },
            { 2 , new MoveCoordinatesContainer { Row = 0, Column = 1 } },
            { 3 , new MoveCoordinatesContainer { Row = -1, Column = 1 } },
            { 4 , new MoveCoordinatesContainer { Row = -1, Column = 0 } },
            { 5 , new MoveCoordinatesContainer { Row = -1, Column = -1 } },
            { 6 , new MoveCoordinatesContainer { Row = 0, Column = -1 } },
            { 7 , new MoveCoordinatesContainer { Row = -1, Column = -1 } },
        };

        public Move(int direction)
        {
            var move = DirectionsTable[direction];
            this.Row = move.Row;
            this.Column = move.Column;
        }

        public Move(int columns, int rows)
        {
            this.Column = columns;
            this.Row = rows;
        }

        public Move GetUnitarian()
        {
            return new Move(this.Column == 0 ? 0 : Math.Abs(this.Column) / this.Column, this.Row == 0 ? 0 : Math.Abs(this.Row) / this.Row);
        }

        public bool IsDiagonal()
        {
            return this.Column != 0 && this.Row != 0;
        }

        public bool IsStraight()
        {
            return this.Column == 0 || this.Row == 0;
        }

        public override string ToString()
        {
            return string.Concat("(", this.Column, ",", this.Row, ")");
        }

        public Move Clone()
        {
            return new Move(Column, Row);
        }
    }
}
