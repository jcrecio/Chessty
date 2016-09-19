namespace Chessty
{
    using System.Linq;
    using System.Runtime.Serialization;
    using Chessty.Hash;
    using Chessty.Movement;
    using Chessty.Pieces;
    using Chessty.Pieces.Black;
    using Chessty.Pieces.White;

    using Chessty.Pieces.Black;
    using Chessty.Pieces.White;
    using Chessty.Enumeration;
    using System;
    using System.Collections.Generic;

    using Chessty;
    using Chessty.Structure;

    [DataContract]
    public class Board
    {
        [DataMember]
        public long HashBoard { get; set; }

        [DataMember]
        public int[,] RandomPieceValues { get; set; }

        public const int MaxMaterial = 23950;

        [DataMember]
        public int MaterialWhite { get; set; }
        [DataMember]
        public int MaterialBlack { get; set; }

        [DataMember]
        public bool WhiteHasCastled { get; set; }
        [DataMember]
        public bool BlackHasCastled { get; set; }

        [DataMember]
        public Square[][] Squares { get; set; }

        [DataMember]
        public Square WhiteKingPosition { get; set; }

        [DataMember]
        public Square BlackKingPosition { get; set; }

        [DataMember]
        public Dictionary<Guid, Square> WhiteSquaresWithPieces { get; set; }

        [DataMember]
        public Dictionary<Guid, Square> BlackSquaresWithPieces { get; set; }

        private delegate Square GetAdjacentSquareDelegate(Square square, int direction, Tuple<int, int>[] combinations);

        public Board()
        {
            this.WhiteSquaresWithPieces = new Dictionary<Guid, Square>();
            this.BlackSquaresWithPieces = new Dictionary<Guid, Square>();

            this.Squares = new Square[8][];
            for (int i = 0; i < 8; i++)
            {
                this.Squares[i] = new Square[8];
                for (int j = 0; j < 8; j++)
                {
                    this.Squares[i][j] = new Square(i, j);
                }
            }

            this.PopulateBoard();

            this.MaterialWhite = MaxMaterial;
            this.MaterialBlack = MaxMaterial;

            Random r = new Random();
            var values = Enumerable
                .Range(1, 12 * 64)
                .OrderBy(n => r.Next())
                .Select(n => n * 1000)
                .ToArray();

            RandomPieceValues = new int[12, 64];
            for (int i = 0; i < 12; i++)
            {
                for (short j = 0; j < 64; j++)
                {
                    var value = values[i * 4 + j];
                    RandomPieceValues[i, j] = value;
                }
            }

            // 0 white rock, 1 white knight, 2 white bishop, 3 white king, 4 white queen, 5 white pawn
            // 6 black rock, 7 black knight, 8 black bishop, 9 black king, 10 black queen, 11 black pawn

            HashBoard ^= RandomPieceValues[0, 0];
            HashBoard ^= RandomPieceValues[1, 1];
            HashBoard ^= RandomPieceValues[2, 2];
            HashBoard ^= RandomPieceValues[3, 3];
            HashBoard ^= RandomPieceValues[4, 4];
            HashBoard ^= RandomPieceValues[2, 5];
            HashBoard ^= RandomPieceValues[1, 6];
            HashBoard ^= RandomPieceValues[0, 7];
            for (short column = 0; column < 7; column++)
            {
                HashBoard ^= RandomPieceValues[5, 8 + column];
            }
            HashBoard ^= RandomPieceValues[0, 56];
            HashBoard ^= RandomPieceValues[1, 57];
            HashBoard ^= RandomPieceValues[2, 58];
            HashBoard ^= RandomPieceValues[3, 59];
            HashBoard ^= RandomPieceValues[4, 60];
            HashBoard ^= RandomPieceValues[2, 61];
            HashBoard ^= RandomPieceValues[1, 62];
            HashBoard ^= RandomPieceValues[0, 63];
            for (short column = 0; column < 7; column++)
            {
                HashBoard ^= RandomPieceValues[11, 48 + column];
            }
        }

        private void PopulateBoard()
        {
            var whiteKingSquare = this.Squares[4][0];
            WhiteKing.Create(whiteKingSquare);
            this.SetPieceFromSquare(whiteKingSquare, PieceColor.White);
            this.WhiteKingPosition = whiteKingSquare;

            var blackKingSquare = this.Squares[4][7];
            BlackKing.Create(blackKingSquare);
            this.SetPieceFromSquare(blackKingSquare, PieceColor.Black);
            this.BlackKingPosition = blackKingSquare;

            var whiteQueen = this.Squares[3][0];
            WhiteQueen.Create(whiteQueen);
            this.SetPieceFromSquare(whiteQueen, PieceColor.White);

            var blackQueen = this.Squares[3][7];
            BlackQueen.Create(blackQueen);
            this.SetPieceFromSquare(blackQueen, PieceColor.Black);

            var whiteRock1 = this.Squares[0][0];
            WhiteRock.Create(whiteRock1);
            this.SetPieceFromSquare(whiteRock1, PieceColor.White);
            var whiteRock2 = this.Squares[7][0];
            WhiteRock.Create(whiteRock2);
            this.SetPieceFromSquare(whiteRock2, PieceColor.White);

            var blackRock1 = this.Squares[0][7];
            BlackRock.Create(blackRock1);
            this.SetPieceFromSquare(blackRock1, PieceColor.Black);
            var blackRock2 = this.Squares[7][7];
            BlackRock.Create(blackRock2);
            this.SetPieceFromSquare(blackRock2, PieceColor.Black);

            var whiteBishop1 = this.Squares[2][0];
            WhiteBishop.Create(whiteBishop1);
            this.SetPieceFromSquare(whiteBishop1, PieceColor.White);
            var whiteBishop2 = this.Squares[5][0];
            WhiteBishop.Create(whiteBishop2);
            this.SetPieceFromSquare(whiteBishop2, PieceColor.White);

            var blackBishop1 = this.Squares[2][7];
            BlackBishop.Create(blackBishop1);
            this.SetPieceFromSquare(blackBishop1, PieceColor.Black);
            var blackBishop2 = this.Squares[5][7];
            BlackBishop.Create(blackBishop2);
            this.SetPieceFromSquare(blackBishop2, PieceColor.Black);

            var whiteKnight1 = this.Squares[1][0];
            WhiteKnight.Create(whiteKnight1);
            this.SetPieceFromSquare(whiteKnight1, PieceColor.White);
            var whiteKnight2 = this.Squares[6][0];
            WhiteKnight.Create(whiteKnight2);
            this.SetPieceFromSquare(whiteKnight2, PieceColor.White);

            var blackKnight1 = this.Squares[1][7];
            BlackKnight.Create(blackKnight1);
            this.SetPieceFromSquare(blackKnight1, PieceColor.Black);
            var blackKnight2 = this.Squares[6][7];
            BlackKnight.Create(blackKnight2);
            this.SetPieceFromSquare(blackKnight2, PieceColor.Black);

            for (int i = 0; i < 8; i++)
            {
                var whitePawn = this.Squares[i][1];
                var blackPawn = this.Squares[i][6];

                WhitePawn.Create(whitePawn);
                BlackPawn.Create(blackPawn);

                this.SetPieceFromSquare(whitePawn, PieceColor.White);
                this.SetPieceFromSquare(blackPawn, PieceColor.Black);
            }
        }

        private void SetPieceFromSquare(Square square, PieceColor color)
        {
            if (color.Equals(PieceColor.White))
            {
                this.WhiteSquaresWithPieces.Add(square.Identifier, square);
            }
            else
            {
                this.BlackSquaresWithPieces.Add(square.Identifier, square);
            }
        }
        private void SetPieceFromSquare(Square square)
        {
            this.SetPieceFromSquare(square, square.CurrentPiece.Color);
        }

        public Square GetSquare(int column, int row)
        {
            if(column >= 0 && column <= 7 && row >= 0 && row <= 7)
            {
                return this.Squares[column][row];
            }

            throw new IndexOutOfRangeException(
                string.Format("The column or row specified are out of the valid range of the board. Row:{0}, Column:{1}", 
                column, row));
        }
        public Square GetAdjacentSquare(Square square, int direction, Type directionType)
        {
            Tuple<int, int>[] directionCombinations;
            GetAdjacentSquareDelegate getAdjacentSquareDelegate;

            if (typeof(Direction) == directionType)
            {
                directionCombinations = adjacentCombinations;
                getAdjacentSquareDelegate = this.GetAdjacentSquare;
            }
            else
            {
                directionCombinations = adjacentKnightCombinations;
                getAdjacentSquareDelegate = this.GetAdjacentSquareKnight;
            }

            return getAdjacentSquareDelegate(square, direction, directionCombinations);
        }
        public Square GetAdjacentSquare(Square square, int direction, Tuple<int, int>[] combinations)
        {
            switch (direction)
            {
                case 0:
                    if (square.Row >= 7)
                    {
                        return null;
                    }
                    break;
                case 1:
                    if (square.Row >= 7 || square.Column >= 7)
                    {
                        return null;
                    }
                    break;
                case 2:
                    if (square.Column >= 7)
                    {
                        return null;
                    }
                    break;
                case 3:
                    if (square.Row <= 0 || square.Column >= 7)
                    {
                        return null;
                    }
                    break;
                case 4:
                    if (square.Row <= 0)
                    {
                        return null;
                    }
                    break;
                case 5:
                    if (square.Row <= 0 || square.Column <= 0)
                    {
                        return null;
                    }
                    break;
                case 6:
                    if (square.Column <= 0)
                    {
                        return null;
                    }
                    break;
                case 7:
                    if (square.Row >= 7 || square.Column <= 0)
                    {
                        return null;
                    }
                    break;
            }

             return this.Squares[square.Column + combinations[direction].Item1][square.Row + combinations[direction].Item2];
        }
        public Square GetAdjacentSquareKnight(Square square, int direction, Tuple<int, int>[] combinations)
        {
            switch (direction)
            {
                case 0:
                    if (square.Column >= 7 || square.Row >= 6)
                    {
                        return null;
                    }
                    break;
                case 1:
                    if (square.Row >= 7 || square.Column >= 6)
                    {
                        return null;
                    }
                    break;
                case 2:
                    if (square.Row <= 1 || square.Column >= 6)
                    {
                        return null;
                    }
                    break;
                case 3:
                    if (square.Row <= 1 || square.Column >= 7)
                    {
                        return null;
                    }
                    break;
                case 4:
                    if (square.Row <= 1 || square.Column <= 1)
                    {
                        return null;
                    }
                    break;
                case 5:
                    if (square.Row <= 0 || square.Column <= 1)
                    {
                        return null;
                    }
                    break;
                case 6:
                    if (square.Row >= 6 || square.Column <= 1)
                    {
                        return null;
                    }
                    break;
                case 7:
                    if (square.Row >= 6 || square.Column <= 0)
                    {
                        return null;
                    }
                    break;
            }

            return this.Squares[square.Column + combinations[direction].Item1][square.Row + combinations[direction].Item2];
        }

        public List<Square> GetAdjacentSquares(Square square, Type directionType)
        {
            var list = new List<Square>();

            for (int i = 0; i < 8; i++)
            {
                var adjacentSquare = this.GetAdjacentSquare(square, i, directionType);
                if (adjacentSquare != null)
                {
                    list.Add(adjacentSquare);
                }
            }

            return list;
        }

        public bool SquaresRangeEmpty(int rowTo, int columnTo, Move iterableMove, Square squareFrom)
        {
            Square squareTo = this.GetSquare(squareFrom.Column + iterableMove.Column, squareFrom.Row + iterableMove.Row);

            while (squareTo.Row != rowTo || squareTo.Column != columnTo)
            {                
                if (squareTo.CurrentPiece != null)
                {
                    return false;
                }

                squareTo = this.GetSquare(squareTo.Column + iterableMove.Column, squareTo.Row + iterableMove.Row);
            }

            var currentPiece = squareFrom.CurrentPiece;
            if (currentPiece != null)
            {
                if ((currentPiece is Bishop
                        || currentPiece is Queen
                        || currentPiece is King) 
                        && iterableMove.IsDiagonal())
                {
                    return true;
                }

                if ((currentPiece is Rock
                    || currentPiece is Queen
                    || currentPiece is King)
                    && iterableMove.IsStraight())
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        public bool IsValidSquare(int column, int row)
        {
            return column >= 0 && column <= BoardSize.MaxColumnIndex && row >= 0 && row <= BoardSize.MaxRowIndex;
        }

        public bool IsValidSquare(Square square)
        {
            return square.Column >= 0 && square.Column <= BoardSize.MaxColumnIndex && square.Row >= 0 && square.Row <= BoardSize.MaxRowIndex;
        }

        public Square GetNearestSquare(Square square, int direction)
        {
            Move move = new Move(direction);
            return this.LookingNearestSquare(square, move);
        }

        public Square GetNearestSquare(Square square, Move move)
        {
            return this.LookingNearestSquare(square, move);
        }

        private Square LookingNearestSquare(Square square, Move move)
        {
            Square squareReached = square;
            var columns = squareReached.Column;
            var rows = squareReached.Row;

            while (this.IsValidSquare(columns, rows) && squareReached.CurrentPiece == null)
            {
                squareReached = this.GetSquare(columns, rows);
                columns = squareReached.Column + move.Column;
                rows = squareReached.Row + move.Row;
            }

            return squareReached;
        }

        public ulong GetHashBoard()
        {
            return 1;
        }

        private static readonly Tuple<int, int>[] adjacentCombinations =
            {
                new Tuple<int, int>(0, 1), 
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 0), 
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, 0), 
                new Tuple<int, int>(-1, 1)
            };

        private static readonly Tuple<int, int>[] adjacentKnightCombinations =
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(1, -2),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-1, 2)
            };
    }
}
