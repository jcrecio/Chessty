namespace Chessty.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Chessty.Abstract;
    using Chessty.Enumeration;
    using Chessty.Hash;
    using Chessty.Matrix;
    using Chessty.Movement;
    using Chessty.Pieces;
    using Chessty.Pieces.Black;
    using Chessty.Pieces.White;
    using Chessty.Structure;

    public class AIManager
    {
        #region Const
        private const float Minimum = -20000;
        private const float Maximum = 20000;
        private const float IntValueKing = 20000;
        private const float ValueForDevelopingKnight = 10;
        private const float ValueForDevelopingBishop = 10;
        private const float ValueForDevelopingQueen = 4;
        private const float ValueForNumAttackersGreaterThanNumDefenders = 0.25f;
        private const float ReductionFactorForAttackerPiece = 0.25f;
        private const float ValueForKnightBlockingPawn = 1;
        private const float ValueForBishopInSameDiagonalThanOppositeKing = 0.25f;
        private const float ValueForRockInSameColumnThanOppositeKing = 0.25f;
        private const float ValueForRockInSameRowThanOppositeKing = 0.25f;
        private const float ValueForRockInPenultimRow = 1;
        private const float ValueForQueenInSameDiagonalThanOppositeKing = 0.25f;
        private const float ValueForQueenInSameColumnThanOppositeKing = 0.25f;
        private const float ValueForQueenInSameRowThanOppositeKing = 0.25f;
        private const double DiscountValueForBorderPawns = 0.12;
        private const float ValueWhenOppositeKingCatchYouOverQueening = 0.5f;
        private const float ValueForPassedPawn = 1;
        private const int KeepQuiescenceSeach = 3;
        private const float ValueCastleEvaluation = 50;
        private const float ValueFactorForAdditionRowToPassedPawn = 1;
        private const int ValueForSamePieceMoved = 10;
        private const int ReductionNullMoveDepth = 2;

        private const int MoveCannotBeDone = 0;
        private const int MoveIsNormal = 1;
        private const int MoveIsCapture = 2;

        private const bool MaxNode = true;
        private const bool MinNode = false;

        private static int here = 0;
        #endregion

        #region Properties
        private Guid PreviousWhitePieceMoved { get; set; }
        private Guid PreviousBlackPieceMoved { get; set; }

        public readonly Matrix kingTableWhite = new Matrix(
                new short[]
                    {
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -20,-30,-30,-40,-40,-30,-30,-20,
                        -10,-20,-20,-20,-20,-20,-20,-10,
                         20, 20,  0,  0,  0,  0, 20, 20,
                         20, 30, 10,  0,  0, 10, 30, 20
                    }
                    .Reverse().ToArray());

        public readonly Matrix kingTableEndGameWhite = new Matrix(
            new short[]
                    {
                        -50,-40,-30,-20,-20,-30,-40,-50,
                        -30,-20,-10,  0,  0,-10,-20,-30,
                        -30,-10, 20, 30, 30, 20,-10,-30,
                        -30,-10, 30, 40, 40, 30,-10,-30,
                        -30,-10, 30, 40, 40, 30,-10,-30,
                        -30,-10, 20, 30, 30, 20,-10,-30,
                        -30,-30,  0,  0,  0,  0,-30,-30,
                        -50,-30,-30,-30,-30,-30,-30,-50
                    });

        public readonly Matrix kingTableBlack = new Matrix(
            new short[]
                    {
                         20, 30, 10,  0,  0, 10, 30, 20,
                         20, 20,  0,  0,  0,  0, 20, 20,
                        -10,-20,-20,-20,-20,-20,-20,-10,
                        -20,-30,-30,-40,-40,-30,-30,-20,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                        -30,-40,-40,-50,-50,-40,-40,-30,
                    }.Reverse().ToArray());

        public readonly Matrix kingTableEndGameBlack = new Matrix(
            new short[]
                    {
                        -50,-30,-30,-30,-30,-30,-30,-50,
                         30,-30,  0,  0,  0,  0,-30,-30,
                        -30,-10, 20, 30, 30, 20,-10,-30,
                        -30,-10, 30, 40, 40, 30,-10,-30,
                        -30,-10, 30, 40, 40, 30,-10,-30,
                        -30,-10, 20, 30, 30, 20,-10,-30,
                        -30,-20,-10,  0,  0,-10,-20,-30,
                        -50,-40,-30,-20,-20,-30,-40,-50,
                    }
                .Reverse().ToArray());

        [DataMember]
        public GamePhase GamePhase { get; set; }

        [DataMember]
        public Matrix WhitePawnMatrix { get; set; }

        [DataMember]
        public Matrix BlackPawnMatrix { get; set; }

        [DataMember]
        public Matrix WhiteBishopMatrix { get; set; }

        [DataMember]
        public Matrix BlackBishopMatrix { get; set; }

        [DataMember]
        public Matrix WhiteRockMatrix { get; set; }

        [DataMember]
        public Matrix BlackRockMatrix { get; set; }

        [DataMember]
        public Matrix WhiteKnightMatrix { get; set; }

        [DataMember]
        public Matrix BlackKnightMatrix { get; set; }

        [DataMember]
        public Matrix WhiteQueenMatrix { get; set; }

        [DataMember]
        public Matrix BlackQueenMatrix { get; set; }

        [DataMember]
        public Matrix WhiteKingMatrix { get; set; }

        [DataMember]
        public Matrix BlackKingMatrix { get; set; }

        [DataMember]
        private Dictionary<Type, short> correspondingPieceTable = new Dictionary<Type, short>();
        #endregion

        [DataMember]
        public Dictionary<long, HashEntry> TranspositionTable { get; set; }

        [DataMember]
        public Dictionary<long, RefutationEntry> KillerMovesTable { get; set; }

        public bool EnableTranspositionTable { get; set; }
        public bool PositionalEvaluationConsideration { get; set; }
        public readonly int[] ColumnsAmountPawnsDefaultValue = { 0, 0, 0, 0, 0, 0, 0, 0 };
        public readonly int[] ColumnsAmountPawnsOppositeValue = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public AIManager()
        {
            correspondingPieceTable.Add(typeof(WhiteRock), 0);
            correspondingPieceTable.Add(typeof(WhiteKnight), 1);
            correspondingPieceTable.Add(typeof(WhiteBishop), 2);
            correspondingPieceTable.Add(typeof(WhiteQueen), 3);
            correspondingPieceTable.Add(typeof(WhiteKing), 4);
            correspondingPieceTable.Add(typeof(WhitePawn), 5);

            correspondingPieceTable.Add(typeof(BlackRock), 6);
            correspondingPieceTable.Add(typeof(BlackKnight), 7);
            correspondingPieceTable.Add(typeof(BlackBishop), 8);
            correspondingPieceTable.Add(typeof(BlackQueen), 9);
            correspondingPieceTable.Add(typeof(BlackKing), 10);
            correspondingPieceTable.Add(typeof(BlackPawn), 11);

            TranspositionTable = new Dictionary<long, HashEntry>();
            KillerMovesTable = new Dictionary<long, RefutationEntry>();

            #region Matrix initialization

            short[] pawnTableWhite =
                new short[]
                    {
                        0,  0,  0,  0,  0,  0,  0,  0,
                        50, 50, 50, 50, 50, 50, 50, 50,
                        10, 10, 20, 30, 30, 20, 10, 10,
                         5,  5, 10, 25, 25, 10,  5,  5,
                         0,  0,  0, 20, 20,  0,  0,  0,
                         5, -5,-10,  0,  0,-10, -5,  5,
                         5, 10, 10,-20,-20, 10, 10,  5,
                         0,  0,  0,  0,  0,  0,  0,  0
                    }.Reverse().ToArray();

            short[] knightTableWhite =
                new short[]
                    {
                        -50,-40,-30,-30,-30,-30,-40,-50,
                        -40,-20,  0,  0,  0,  0,-20,-40,
                        -30,  0, 10, 15, 15, 10,  0,-30,
                        -30,  5, 15, 20, 20, 15,  5,-30,
                        -30,  0, 15, 20, 20, 15,  0,-30,
                        -30,  5, 10, 15, 15, 10,  5,-30,
                        -40,-20,  0,  5,  5,  0,-20,-40,
                        -50,-40,-30,-30,-30,-30,-40,-50,
                    }.Reverse().ToArray();

            short[] bishopTableWhite =
                new short[]
                    {
                        -20,-10,-10,-10,-10,-10,-10,-20,
                        -10,  0,  0,  0,  0,  0,  0,-10,
                        -10,  0,  5, 10, 10,  5,  0,-10,
                        -10,  5,  5, 10, 10,  5,  5,-10,
                        -10,  0, 10, 10, 10, 10,  0,-10,
                        -10, 10, 10, 10, 10, 10, 10,-10,
                        -10,  5,  0,  0,  0,  0,  5,-10,
                        -20,-10,-10,-10,-10,-10,-10,-20,
                    }.Reverse().ToArray();

            short[] rockTableWhite =
                new short[]
                    {
                          0,  0,  0,  0,  0,  0,  0,  0,
                          5, 10, 10, 10, 10, 10, 10,  5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                          0,  0,  0,  5,  5,  0,  0,  0
                    }.Reverse().ToArray();

            short[] queenTableWhite =
                new short[]
                    {
                        -20,-10,-10, -5, -5,-10,-10,-20,
                        -10,  0,  0,  0,  0,  0,  0,-10,
                        -10,  0,  5,  5,  5,  5,  0,-10,
                         -5,  0,  5,  5,  5,  5,  0, -5,
                          0,  0,  5,  5,  5,  5,  0, -5,
                        -10,  5,  5,  5,  5,  5,  0,-10,
                        -10,  0,  0,  0,  0,  5,  0,-10,
                        -20,-10,-10, -5, -5,-10,-10,-20
                    }.Reverse().ToArray();

            short[] pawnTableBlack = new short[]
                    {
                        0,  0,  0,  0,  0,  0,  0,  0,
                        5, 10, 10,-20,-20, 10, 10,  5,
                        5, -5,-10,  0,  0,-10, -5,  5,
                        0,  0,  0, 20, 20,  0,  0,  0,
                        5,  5, 10, 25, 25, 10,  5,  5,
                        10, 10, 20, 30, 30, 20, 10, 10,
                        50, 50, 50, 50, 50, 50, 50, 50,
                        0,  0,  0,  0,  0,  0,  0,  0,
                    }.Reverse().ToArray();

            short[] knightTableBlack = new short[]
                    {
                        -50,-40,-30,-30,-30,-30,-40,-50,
                        -40,-20,  0,  5,  5,  0,-20,-40,
                        -30,  5, 10, 15, 15, 10,  5,-30,
                        -30,  0, 15, 20, 20, 15,  0,-30,
                        -30,  5, 15, 20, 20, 15,  5,-30,
                        -30,  0, 10, 15, 15, 10,  0,-30,
                        -40,-20,  0,  0,  0,  0,-20,-40,
                        -50,-40,-30,-30,-30,-30,-40,-50,
                    }.Reverse().ToArray();

            var bishopTableBlack = new short[]
                    {
                        -20,-10,-10,-10,-10,-10,-10,-20,
                        -10,  5,  0,  0,  0,  0,  5,-10,
                        -10, 10, 10, 10, 10, 10, 10,-10,
                        -10,  0, 10, 10, 10, 10,  0,-10,
                        -10,  5,  5, 10, 10,  5,  5,-10,
                        -10,  0,  5, 10, 10,  5,  0,-10,
                        -10,  0,  0,  0,  0,  0,  0,-10,
                        -20,-10,-10,-10,-10,-10,-10,-20,
                    }.Reverse().ToArray();

            var rockTableBlack = new short[]
                    {
                          0,  0,  0,  5,  5,  0,  0,  0,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                         -5,  0,  0,  0,  0,  0,  0, -5,
                          5, 10, 10, 10, 10, 10, 10,  5,
                          0,  0,  0,  0,  0,  0,  0,  0,
                    }.Reverse().ToArray();


            short[] queenTableBlack = new short[]
                    {
                        -20,-10,-10, -5, -5,-10,-10,-20,
                        -10,  0,  0,  0,  0,  5,  0,-10,
                        -10,  0,  5,  5,  5,  5,  5,-10,
                        -10,  0,  5,  5,  5,  5,  5,-10,
                         -5,  0,  5,  5,  5,  5,  0,  0,
                        -10,  0,  5,  5,  5,  5,  0,-10,
                        -10,  0,  0,  0,  0,  0,  0,-10,
                        -20,-10,-10, -5, -5,-10,-10,-20,
                    }.Reverse().ToArray();

            WhitePawnMatrix = new Matrix(pawnTableWhite);
            BlackPawnMatrix = new Matrix(pawnTableBlack);
            WhiteBishopMatrix = new Matrix(bishopTableWhite);
            BlackBishopMatrix = new Matrix(bishopTableBlack);
            WhiteRockMatrix = new Matrix(rockTableWhite);
            BlackRockMatrix = new Matrix(rockTableBlack);
            WhiteKnightMatrix = new Matrix(knightTableWhite);
            BlackKnightMatrix = new Matrix(knightTableBlack);
            WhiteQueenMatrix = new Matrix(queenTableWhite);
            BlackQueenMatrix = new Matrix(queenTableBlack);
            WhiteKingMatrix = kingTableWhite;
            BlackKingMatrix = kingTableBlack;

            #endregion
        }

        // 0 cannot be done, 1 normal, >=2 capture
        public int GetMoveByPriority(Move move, Board board, Square square, bool inCheckBeforeMove)
        {
            var squareFromColumn = square.Column;
            var squareFromRow = square.Row;

            var squareToColumn = squareFromColumn + move.Column;
            var squareToRow = squareFromRow + move.Row;

            if (!board.IsValidSquare(squareToColumn, squareToRow))
            {
                return MoveCannotBeDone;
            }

            var pieceFrom = square.CurrentPiece;
            var colorFrom = pieceFrom.Color;

            var squareTo = board.GetSquare(squareToColumn, squareToRow);
            var squareToCurrentPieceIsNull = squareTo.CurrentPiece == null;

            if (!squareToCurrentPieceIsNull && colorFrom == squareTo.CurrentPiece.Color)
            {
                return MoveCannotBeDone;
            }

            if (pieceFrom.Value == PieceValue.Pawn)
            {
                return this.GetPawnMoveByPriority(move, board, squareTo, colorFrom, pieceFrom);
            }

            if (pieceFrom.Value == PieceValue.King)
            {
                var kingCanMove =  this.GetKingMoveByPriority(move, board, square, inCheckBeforeMove, pieceFrom);
                if (kingCanMove != MoveCannotBeDone)
                {
                    return kingCanMove;
                }
            }

            var valuePieceInSquareTo = squareToCurrentPieceIsNull ? 0 :squareTo.CurrentPiece.Value;

            if (pieceFrom.Value == PieceValue.Knight)
            {
                return this.GetKnightMoveByPriority(valuePieceInSquareTo);
            }

            var iterableMove = move.GetUnitarianVector() as Move; //TODO: IMPROVE SPEED
            if (board.SquaresRangeEmpty(squareTo.Row, squareTo.Column, iterableMove, square))
            {
                return this.GetMoveNormalByPriority(valuePieceInSquareTo, pieceFrom);
            }

            return MoveCannotBeDone;
        }

        private int GetMoveNormalByPriority(PieceValue valuePieceInSquareTo, Piece pieceFrom)
        {
            if (valuePieceInSquareTo == 0)
            {
                return MoveIsNormal;
            }

            if (pieceFrom.Value == PieceValue.Queen)
            {
                return MoveIsNormal;
            }

            if (valuePieceInSquareTo > pieceFrom.Value)
            {
                return 2 + valuePieceInSquareTo - pieceFrom.Value;
            }

            return MoveIsCapture;
        }

        private int GetKnightMoveByPriority(PieceValue valuePieceInSquareTo)
        {
            if (valuePieceInSquareTo == 0)
            {
                return MoveIsNormal;
            }

            if (valuePieceInSquareTo > PieceValue.Knight)
            {
                return 2 + valuePieceInSquareTo - PieceValue.Knight;
            }

            return MoveIsCapture;
        }

        private int GetKingMoveByPriority(Move move, Board board, Square square, bool inCheckBeforeMove, Piece pieceFrom)
        {
            if (!inCheckBeforeMove && move.MoveType == 2 && (move as KingMove).Castling)
            {
                var canCastle = pieceFrom.Color == PieceColor.White
                    ? !board.WhiteHasCastled
                    : !board.BlackHasCastled && (pieceFrom as King).Castle;

                if (canCastle)
                {
                    var row = square.CurrentPiece.Color == PieceColor.White ? 0 : 7;

                    if ((move.Column == 2 && board.GetSquare(5, row).CurrentPiece == null
                         && board.GetSquare(6, row).CurrentPiece == null)
                        || (move.Column == -2 && board.GetSquare(1, row).CurrentPiece == null
                            && board.GetSquare(2, row).CurrentPiece == null
                            && board.GetSquare(3, row).CurrentPiece == null))
                    {
                        return 4;
                    }
                }
            }

            return 0;
        }

        private int GetPawnMoveByPriority(Move move, Board board, Square squareTo, PieceColor colorFrom, Piece pieceFrom)
        {
            var pawnMove = move as PawnMove;
            var colorEqualsWhite = colorFrom == PieceColor.White;

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
                && (pieceTo != null && pieceTo.Color != colorFrom))
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

            if ((pawnType == PawnMoveType.AdvanceTwo && pawnFrom.AdvanceTwo)&& board.GetSquare(squareTo.Column, colorEqualsWhite ? 2 : 5).CurrentPiece == null
                    && pieceTo == null)
            {
                return 2;
            }

            return 0;
        }

        private bool IsAttacked(Board board, Square square, int turn, bool turnEqualOne)
        {
            var oppositeColor = turnEqualOne ? PieceColor.Black : PieceColor.White;

            var adjacents = board.GetAdjacentSquares(square, typeof (Direction));

            foreach (var adjacent in adjacents)
            {
                var currentPiece = adjacent.CurrentPiece;

                var columnAMinusColumnB = adjacent.Column - square.Column;
                var rowAMinusRowB = adjacent.Row - square.Row;
                var difColumn = Math.Abs(columnAMinusColumnB);
                var difRow = Math.Abs(rowAMinusRowB);

                var diagonal = difColumn == 1 && difRow == 1;
                var face = difColumn + difRow == 1;

                if (board.IsValidSquare(adjacent.Row, adjacent.Column) && (currentPiece != null)
                    && currentPiece.Color == oppositeColor)
                {
                    var currentPieceValue = currentPiece.Value;

                    if (currentPieceValue == PieceValue.Pawn)
                    {
                        if ((difColumn == 1) && turnEqualOne
                            ? adjacent.Row - 1 == square.Row
                            : adjacent.Row + 1 == square.Row)
                        {
                            return true;
                        }
                    }

                    var diaFace = diagonal || face;

                    if ((currentPieceValue == PieceValue.King && diaFace)
                        || (currentPieceValue == PieceValue.Queen && diaFace)
                        || (currentPieceValue == PieceValue.Rock && face)
                        || (currentPieceValue == PieceValue.Bishop && diagonal))
                    {
                        return true;
                    }
                }

                if (currentPiece != null)
                {
                    continue;
                }

                Move move = new Move(columnAMinusColumnB, rowAMinusRowB);
                Square nearestSquare = board.GetNearestSquare(adjacent, move);

                if (nearestSquare == null || nearestSquare.CurrentPiece == null)
                {
                    continue;
                }

                var nearestSquarePiece = nearestSquare.CurrentPiece;
                var nearestSquarePieceValue = nearestSquare.CurrentPiece.Value;
                var isOpposite = nearestSquarePiece.Color == oppositeColor;

                var isQueen = nearestSquarePieceValue == PieceValue.Queen;
                if (diagonal && isOpposite && (isQueen || nearestSquarePieceValue == PieceValue.Bishop))
                {
                    return true;
                }

                if (face && isOpposite && (isQueen || nearestSquarePieceValue == PieceValue.Rock))
                {
                    return true;
                }
            }

            var adjacentsKnight = board.GetAdjacentSquares(square, typeof (KnightDirection));
            foreach (var adjacent in adjacentsKnight)
            {
                var currentPiece = adjacent.CurrentPiece;

                if (board.IsValidSquare(adjacent.Row, adjacent.Column)
                    && currentPiece != null
                    && currentPiece.Color == oppositeColor
                    && currentPiece.Value == PieceValue.Knight)
                {
                    return true;
                }
            }

            return false;
        }

        private int[] GetAttackingAndDefendingValues(Board board, Square square, int turn, bool turnEqualOne, out int[] defendedBy, out int countAttackers, out int countDefenders)
        {
            var defendedValues = new List<int>();
            var attackingValues = new List<int>();

            var oppositeColor = turnEqualOne ? PieceColor.Black : PieceColor.White;

            var adjacents = board.GetAdjacentSquares(square, typeof(Direction));
            countDefenders = 0;
            countAttackers = 0;

            foreach (var adjacent in adjacents)
            {
                var currentPiece = adjacent.CurrentPiece;

                var columnAMinusColumnB = adjacent.Column - square.Column;
                var rowAMinusRowB = adjacent.Row - square.Row;
                var difColumn = Math.Abs(columnAMinusColumnB);
                var difRow = Math.Abs(rowAMinusRowB);

                var diagonal = difColumn == 1 && difRow == 1;
                var face = difColumn + difRow == 1;

                if (board.IsValidSquare(adjacent.Row, adjacent.Column) && (currentPiece != null))
                {
                    var currentPieceValue = currentPiece.Value;

                    if (currentPieceValue == PieceValue.Pawn)
                    {
                        if (difColumn == 1 && turnEqualOne
                                ? adjacent.Row - 1 == square.Row
                                : adjacent.Row + 1 == square.Row)
                        {
                            if (currentPiece.Color == oppositeColor)
                            {
                                attackingValues.Add((int)PieceValue.Pawn);
                                countAttackers++;
                            }
                            else
                            {
                                defendedValues.Add((int)PieceValue.Pawn);
                                countDefenders++;
                            }
                        }
                    }

                    var diaFace = diagonal || face;

                    if ((currentPieceValue == PieceValue.King && diaFace)
                        || (currentPieceValue == PieceValue.Queen && diaFace)
                        || (currentPieceValue == PieceValue.Rock && face)
                        || (currentPieceValue == PieceValue.Bishop && diagonal))
                    {
                        if (currentPiece.Color == oppositeColor)
                        {
                            attackingValues.Add((int)currentPiece.Value);
                        }
                        else
                        {
                            defendedValues.Add((int)currentPiece.Value);
                        }
                    }
                }

                if (currentPiece != null)
                {
                    continue;
                }

                Move move = new Move(columnAMinusColumnB, rowAMinusRowB);
                Square nearestSquare = board.GetNearestSquare(adjacent, move);

                if (nearestSquare == null || nearestSquare.CurrentPiece == null)
                {
                    continue;
                }

                var nearestSquarePiece = nearestSquare.CurrentPiece;
                var nearestSquarePieceValue = nearestSquare.CurrentPiece.Value;
                var isOpposite = nearestSquarePiece.Color == oppositeColor;
                var nearestCurrentPiece = nearestSquare.CurrentPiece;

                var isQueen = nearestCurrentPiece is Queen;
                if (diagonal && isOpposite && (isQueen || nearestSquarePieceValue == PieceValue.Bishop))
                {
                    if (nearestCurrentPiece.Color == oppositeColor)
                    {
                        attackingValues.Add((int)nearestCurrentPiece.Value);
                    }
                    else
                    {
                        defendedValues.Add((int)nearestCurrentPiece.Value);
                    }
                }

                if (!face || !isOpposite || (!isQueen && nearestSquarePieceValue != PieceValue.Rock)) continue;

                if (nearestCurrentPiece.Color == oppositeColor)
                {
                    attackingValues.Add((int)nearestCurrentPiece.Value);
                }
                else
                {
                    defendedValues.Add((int)nearestCurrentPiece.Value);
                }
            }

            var adjacentsKnight = board.GetAdjacentSquares(square, typeof(KnightDirection));
            foreach (var adjacent in adjacentsKnight)
            {
                var currentPiece = adjacent.CurrentPiece;

                if (board.IsValidSquare(adjacent.Row, adjacent.Column) && currentPiece != null
                    && currentPiece.Color.Equals(oppositeColor) && currentPiece.Value == PieceValue.Knight)
                {
                    if (currentPiece.Color == oppositeColor)
                    {
                        attackingValues.Add((int)currentPiece.Value);
                    }
                    else
                    {
                        defendedValues.Add((int)currentPiece.Value);
                    }
                }
            }

            defendedBy = defendedValues.ToArray();
            return attackingValues.ToArray();
        }

        public bool IsCheck(Board board, int turn, bool turnEqualOne)
        {
            Square kingSquare = turnEqualOne ? board.WhiteKingPosition : board.BlackKingPosition;
            return this.IsAttacked(board, kingSquare, turn, turnEqualOne);
        }

        private List<Operator> GetOrderedLegalCandidates(Board board, bool turnEqualOne, bool inCheckBeforeMove, int depth)
        {
            var nonZeroOperators = this.GetLegalCandidates(board, turnEqualOne, inCheckBeforeMove, depth);

            return nonZeroOperators.OrderByDescending(c => c.Type).ToList();
        }

        public IEnumerable<Operator> GetLegalCandidates(Board board, bool turnEqualOne, bool inCheckBeforeMove, int depth)
        {
            var nonZeroOperators = this.GetPossibleCandidates(board, turnEqualOne, inCheckBeforeMove, depth);

            return nonZeroOperators.Where(c => c.Type > 0);
        } 

        private List<Operator> GetPossibleCandidates(Board board, bool turnEqualOne, bool inCheckBeforeMove, int depth)
        {
            var validOperators = this.GetCandidates(board, turnEqualOne, depth);
           
            foreach (var validOperator in validOperators)
            {
                validOperator.Type = this.GetMoveByPriority(validOperator.Move, board,  validOperator.Square, inCheckBeforeMove);
            }

            return validOperators;
        }

        private List<Operator> GetCandidates(Board board, bool turnEqualOne, int depth)
        {
            var squares = (turnEqualOne ? board.WhiteSquaresWithPieces : board.BlackSquaresWithPieces).Values
                            .Where(s => s.CurrentPiece != null);

            var candidates = new List<Operator>();
            foreach (var square in squares)
            {
                var candidatesPerPiece = square.CurrentPiece.GetMoves().Select(p => new Operator(p, square)).ToList();

                if (KillerMovesTable.ContainsKey(board.HashBoard) && KillerMovesTable[board.HashBoard].Depth == depth)
                {
                    var indexKillerMove = candidatesPerPiece.IndexOf(KillerMovesTable[board.HashBoard].Operator);
                    if (indexKillerMove != -1)
                    {
                        candidatesPerPiece[indexKillerMove].Type = 10;
                    }
                }

                candidates.AddRange(candidatesPerPiece);
            }

            return candidates;
        }

        public Square MakeMove(
            Board board,
            Operator candidate,
            out Square sourceSquare,
            out Piece pieceToReset,
            out Piece originalPiece,
            out bool queening,
            out GamePhase gamePhase)
        {
            gamePhase = GamePhase;
            sourceSquare = candidate.Square;

            originalPiece = sourceSquare.CurrentPiece;
            var color = sourceSquare.CurrentPiece.Color;

            Square nextSquare = null;
            XorSquare(board, sourceSquare.Column, sourceSquare.Row, originalPiece);
            nextSquare = candidate.Operate(board, out pieceToReset);

            if (candidate.Move.MoveType == 2 && (candidate.Move as KingMove).Castling)
            {
                this.MakeCastle(board, candidate.Move.Column == 2 ? 1 : 0, color == PieceColor.White ? 0 : 7);
            }

            if (color == PieceColor.White)
            {
                if (nextSquare.CurrentPiece.Value == PieceValue.Pawn && nextSquare.Row == 7)
                {
                    sourceSquare.CurrentPiece = new WhiteQueen(sourceSquare);
                    board.MaterialWhite += (int)PieceValue.Queen;
                    queening = true;
                }
                else
                {
                    queening = false;
                }

                if (pieceToReset != null)
                {
                    XorSquare(board, nextSquare.Column, nextSquare.Row, nextSquare.CurrentPiece);
                    board.MaterialBlack -= (int)pieceToReset.Value;
                }

                var nextValue = nextSquare.CurrentPiece.Value;
                if (nextValue == PieceValue.Rock || nextValue == PieceValue.King)
                {
                    (board.WhiteKingPosition.CurrentPiece as King).Castle = false;
                }

                board.WhiteSquaresWithPieces.Remove(sourceSquare.Identifier);
                board.WhiteSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
                board.BlackSquaresWithPieces.Remove(nextSquare.Identifier);

                if (board.MaterialWhite <= 21700 && board.MaterialBlack <= 21700)
                {
                    GamePhase = GamePhase.EndGame;

                    this.WhiteKingMatrix = kingTableEndGameWhite;
                }
            }
            else
            {
                var nextValue = nextSquare.CurrentPiece.Value;

                if (nextValue == PieceValue.Pawn && nextSquare.Row == 0)
                {
                    sourceSquare.CurrentPiece = new BlackQueen(sourceSquare);
                    board.MaterialBlack += (int)PieceValue.Queen;
                    queening = true;
                }
                else
                {
                    queening = false;
                }

                if (pieceToReset != null)
                {
                    XorSquare(board, nextSquare.Column, nextSquare.Row, nextSquare.CurrentPiece);
                    board.MaterialWhite -= (int)pieceToReset.Value;
                }

                if (sourceSquare.CurrentPiece != null)
                {
                    var value = sourceSquare.CurrentPiece.Value;

                    if (value == PieceValue.Rock || value == PieceValue.King)
                    {
                        (board.BlackKingPosition.CurrentPiece as King).Castle = false;
                    }
                }

                board.BlackSquaresWithPieces.Remove(sourceSquare.Identifier);
                board.BlackSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
                board.WhiteSquaresWithPieces.Remove(nextSquare.Identifier);

                if (board.MaterialWhite <= 21700 && board.MaterialBlack <= 21700)
                {
                    GamePhase = GamePhase.EndGame;

                    this.BlackKingMatrix = kingTableEndGameBlack;
                }
            }

            XorSquare(board, nextSquare.Column, nextSquare.Row, nextSquare.CurrentPiece);

            return nextSquare;
        }

        public void XorSquare(Board board, int column, int row, Piece piece)
        {
            var numberOfSquare = (row * 8) + column; // TRY NOT TO WORK WITH TYPES BECAUSE OF ITS SLOW PERFORMANCE?!?!?
            long hashValue = board.RandomPieceValues[correspondingPieceTable[piece.GetType()], numberOfSquare];

            board.HashBoard ^= hashValue;
        }

        private void MakeCastle(Board board, int dir, int row) // dir <- = 0 | 1 = ->
        {
            if (row == 0)
            {
                board.WhiteHasCastled = true;
            }
            else
            {
                board.BlackHasCastled = true;
            }

            var square = dir == 0 ? board.GetSquare(0, row) : board.GetSquare(7, row);
            var move = new Move(dir == 0 ? 3 : -2, 0);
            var op = new Operator(move, square);
            var color = square.CurrentPiece.Color;
            Piece piece;

            var nextSquare = op.Operate(board, out piece);

            XorSquare(board, square.Column, square.Row, nextSquare.CurrentPiece);
            XorSquare(board, nextSquare.Column, nextSquare.Row, nextSquare.CurrentPiece);

            if (color == PieceColor.White)
            {
                board.WhiteSquaresWithPieces.Remove(square.Identifier);
                board.WhiteSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
            }
            else
            {
                board.BlackSquaresWithPieces.Remove(square.Identifier);
                board.BlackSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
            }
        }

        public void UndoMove(
            Board board,
            Operator candidate,
            Square nextSquare,
            Square sourceSquare,
            Piece pieceToReset,
            List<bool> previousValues,
            Piece originalPiece,
            bool queening,
            GamePhase previousPhase,
            long previousHashBoard,
            bool disCastling = false)
        {
            var colorKing = nextSquare.CurrentPiece.Color;

            if (originalPiece != null)
            {
                nextSquare.CurrentPiece = originalPiece;
            }

            candidate.Undo(board, nextSquare, sourceSquare, pieceToReset, previousValues.ToArray());

            if (candidate.Move is KingMove && (candidate.Move as KingMove).Castling)
            {
                this.UndoCastle(board, candidate.Move.Column == 2 ? 1 : 0, colorKing == PieceColor.White ? 0 : 7);
            }

            var sourceSquarePieceValue = sourceSquare.CurrentPiece.Value;
            if (sourceSquare.CurrentPiece.Color == PieceColor.White)
            {
                if (queening)
                {
                    board.MaterialWhite -= (int)PieceValue.Queen;
                }

                if ((sourceSquarePieceValue == PieceValue.Rock || sourceSquarePieceValue == PieceValue.King)
                    && !disCastling)
                {
                    (board.WhiteKingPosition.CurrentPiece as King).Castle = previousValues[0];
                }

                if (!board.WhiteSquaresWithPieces.ContainsKey(sourceSquare.Identifier))
                {
                    board.WhiteSquaresWithPieces.Add(sourceSquare.Identifier, sourceSquare);
                }
                if (pieceToReset != null)
                {
                    board.MaterialBlack += (int)pieceToReset.Value;
                    board.BlackSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
                }
            }
            else
            {
                if (queening)
                {
                    board.MaterialBlack -= (int)PieceValue.Queen;
                }

                if ((sourceSquarePieceValue == PieceValue.Rock || sourceSquarePieceValue == PieceValue.King)
                    && !disCastling)
                {
                    (board.BlackKingPosition.CurrentPiece as King).Castle = previousValues[0];
                }

                if (!board.BlackSquaresWithPieces.ContainsKey(sourceSquare.Identifier))
                {
                    board.BlackSquaresWithPieces.Add(sourceSquare.Identifier, sourceSquare);
                }
                if (pieceToReset != null)
                {
                    board.MaterialWhite += (int)pieceToReset.Value;
                    board.WhiteSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);
                }
            }

            var color = sourceSquare.CurrentPiece.Color;
            if (color == PieceColor.White)
            {
                board.WhiteSquaresWithPieces.Remove(nextSquare.Identifier); 
                GamePhase = previousPhase;
                if (previousPhase != GamePhase.EndGame)
                {
                    this.WhiteKingMatrix = kingTableWhite;
                }
            }
            else
            {
                board.BlackSquaresWithPieces.Remove(nextSquare.Identifier);
                GamePhase = previousPhase;
                if (previousPhase != GamePhase.EndGame)
                {
                    this.BlackKingMatrix = kingTableBlack;
                }
            }

            board.HashBoard = previousHashBoard;
        }

        private void UndoCastle(Board board, int dirCastle, int row)
        {
            var dirEqualZero = dirCastle == 0;
            if (row == 0)
            {
                board.WhiteHasCastled = false;
            }
            else
            {
                board.BlackHasCastled = false;
            }

            var sourceSquare = dirEqualZero ? board.GetSquare(0, row) : board.GetSquare(7, row);
            var nextSquare = dirEqualZero ? board.GetSquare(3, row) : board.GetSquare(5, row);
            var move = new Move(dirCastle == 0 ? 3 : -2, 0);
            var op = new Operator(move, sourceSquare);

            Piece pi = null;
            op.Undo(board, nextSquare, sourceSquare, pi, new bool[] { false });
            var color = sourceSquare.CurrentPiece.Color;

            if (color == PieceColor.White)
            {
                board.WhiteSquaresWithPieces.Add(sourceSquare.Identifier, sourceSquare);
                board.WhiteSquaresWithPieces.Remove(nextSquare.Identifier);
            }
            else
            {
                board.BlackSquaresWithPieces.Add(sourceSquare.Identifier, sourceSquare);
                board.BlackSquaresWithPieces.Remove(nextSquare.Identifier);
            }
        }

        public Operator Search(Board board, int turn, int depth)
        {
            Operator value;
            //if (!previousresult.Equals(Minimum))
            //{
            //    value = this.Search(board, depth, previousresult - (float)PieceValue.Rock, previousresult + (float)PieceValue.Rock, turn);

            //}
            //else
            //{
                value = this.Search(board, depth, Minimum, Maximum, turn);
            //}

            KillerMovesTable.Clear();

            return value;
        }

        private Operator Search(Board board, int depth, float alpha, float beta, int turn)
        {
            long hashBoard = board.HashBoard;
            Operator result = null;
            var turnEqualOne = turn == 1;

            float best = Minimum;

            var inCheckBeforeMove = this.IsCheck(board, turn, turnEqualOne);
            var candidates = this.GetOrderedLegalCandidates(board, turnEqualOne, inCheckBeforeMove, depth);
            var count = candidates.Count;

            if (count == 1)
            {
                result = candidates[0];
            }
            else
            {
                foreach (var candidate in candidates)
                {
                    var square = candidate.Square;
                    var currentPiece = square.CurrentPiece;
                    var currentPieceValue = currentPiece.Value;

                    var previousValues = GetValuesToReset(board, currentPieceValue, currentPiece, turnEqualOne);

                    Square sourceSquare;
                    Piece pieceToReset;
                    Piece originalPiece;
                    bool queening;

                    GamePhase previousPhase;

                    Guid idPieceToCompare = square.CurrentPiece.Id;

                    var nextSquare = this.MakeMove(board, candidate, out sourceSquare, out pieceToReset, out originalPiece, out queening, out previousPhase);

                    if (!this.IsCheck(board, turn, turnEqualOne))
                    {
                        if (result == null)
                        {
                            result = candidate;
                        }

                        var value = -this.NegaMax(board, depth - 1, -beta, -alpha, turn, MinNode, idPieceToCompare, KeepQuiescenceSeach);

                        if (best < value)
                        {
                            best = value;
                            result = candidate;
                        }

                        alpha = Math.Max(alpha, best);

                        this.UndoMove(board, candidate, nextSquare, sourceSquare, pieceToReset, previousValues, originalPiece, queening, previousPhase, hashBoard);

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                    else
                    {
                        this.UndoMove(board, candidate, nextSquare, sourceSquare, pieceToReset, previousValues, originalPiece, queening, previousPhase, hashBoard);
                    }
                }
            }

            if (result != null)
            {
                if (turnEqualOne)
                {
                    this.PreviousWhitePieceMoved = result.Square.CurrentPiece.Id;
                }
                else
                {
                    this.PreviousBlackPieceMoved = result.Square.CurrentPiece.Id;
                }
            }

            return result;
        }

        private List<bool> GetValuesToReset(Board board, PieceValue currentPieceValue, Piece currentPiece, bool turnEqualOne)
        {
            List<bool> previousValues = new List<bool>();
            switch (currentPieceValue)
            {
                case PieceValue.Pawn:
                {
                    var pawn = currentPiece as Pawn;
                    previousValues.AddRange(new[] {pawn.IsPassantLeft, pawn.IsPassantRight, pawn.AdvanceTwo});
                }
                    break;
                case PieceValue.King:
                {
                    var king = currentPiece as King;
                    previousValues.AddRange(new[] {king.Castle});
                }
                    break;
                case PieceValue.Rock:
                {
                        var king =
                            (turnEqualOne ? board.WhiteKingPosition.CurrentPiece : board.BlackKingPosition.CurrentPiece)
                                as King;
                        previousValues.AddRange(new[] {king.Castle});
                }
                    break;
                case PieceValue.Bishop:
                    previousValues.AddRange(new[] {(currentPiece as Bishop).Developed});
                    break;
                case PieceValue.Knight:
                    previousValues.AddRange(new[] {(currentPiece as Knight).Developed});
                    break;
                case PieceValue.Queen:
                    previousValues.AddRange(new[] {(currentPiece as Queen).Developed});
                    break;
            }

            return previousValues;
        }

        private float NegaMax(
            Board board,
            int depth,
            float alpha,
            float beta,
            int turn,
            bool maxOrMin,
            Guid idPieceToCompare,
            int keepQuiescenceSearch,
            bool queening = false,
            bool nullMoveAllow = false)
        {
            long hashBoard = board.HashBoard;

            if (EnableTranspositionTable && TranspositionTable.ContainsKey(hashBoard))
            {
                var hashEntry = TranspositionTable[hashBoard];
                if (hashEntry.Depth >= depth)
                {
                    switch (hashEntry.NodeType)
                    {
                        case NodeType.Exact:
                            return hashEntry.Score;
                        case NodeType.LowerBound:
                            if (hashEntry.Score > alpha)
                            {
                                alpha = hashEntry.Score;
                            }
                            break;
                        case NodeType.UpperBound:
                            if (hashEntry.Score < beta)
                            {
                                beta = hashEntry.Score;
                            }
                            break;
                    }

                    if (alpha >= beta)
                    {
                        return hashEntry.Score;
                    }
                }
            }

            float alphaOrig = alpha;

            var currentTurn = maxOrMin ? turn : -turn;
            var turnEqualOne = currentTurn == 1;

            var inCheckBeforeMove = this.IsCheck(board, turn, turnEqualOne);
            var candidates = this.GetOrderedLegalCandidates(board, turnEqualOne, inCheckBeforeMove, depth);
            var isTerminal = this.MaterialDifference(board) >= IntValueKing;

            if (depth <= 0 || isTerminal)
            {
                var value = this.Evaluate(board, turn, -turn, maxOrMin, candidates.Count, keepQuiescenceSearch, alpha, beta, idPieceToCompare, queening);

                return maxOrMin ? value : -value;
            }

            if (!inCheckBeforeMove && nullMoveAllow)
            {
                float value = -this.NegaMax(board, depth - ReductionNullMoveDepth - 1, -beta, -beta + 1, turn, !maxOrMin, idPieceToCompare, keepQuiescenceSearch);

                if (value > beta)
                {
                    return value;
                }
            }

            float bestValue = Minimum;

            foreach (var candidate in candidates)
            {
                var square = candidate.Square;
                var currentPiece = square.CurrentPiece;
                var currentPieceValue = currentPiece.Value;
                var previousValues = GetValuesToReset(board, currentPieceValue, currentPiece, turnEqualOne);

                Square sourceSquare;
                Piece pieceToReset;
                Piece originalPiece;
                bool queen;
                GamePhase previousPhase;

                var nextSquare = this.MakeMove(board, candidate, out sourceSquare, out pieceToReset, out originalPiece, out queen, out previousPhase);

                if (!this.IsCheck(board, currentTurn, turnEqualOne))
                {
                    float valueBranch = -this.NegaMax(board, queening ? depth : depth - 1, -beta, -alpha, turn, !maxOrMin, idPieceToCompare, KeepQuiescenceSeach, queen, candidate.Type > 1);
                    this.UndoMove(board, candidate, nextSquare, sourceSquare, pieceToReset, previousValues, originalPiece, queening, previousPhase, hashBoard);

                    if (valueBranch > bestValue)
                    {
                        bestValue = valueBranch;
                    }

                    if (bestValue > alpha)
                    {
                        alpha = bestValue;
                    }

                    if (bestValue >= beta)
                    {                        
                        KillerMovesTable[hashBoard] = new RefutationEntry { HashBoard = hashBoard, Operator = candidate.Clone(), Depth = depth };
                        break;
                    }
                }
                else
                {
                    this.UndoMove(board, candidate, nextSquare, sourceSquare, pieceToReset, previousValues, originalPiece, queening, previousPhase, hashBoard);
                }
            }

            if (maxOrMin)
            {
                if (bestValue <= alphaOrig)
                {
                    this.SetHashEntry(board, hashBoard, depth, bestValue, NodeType.UpperBound);
                }
                else
                {
                    this.SetHashEntry(board, hashBoard, depth, bestValue, NodeType.Exact);
                }
            }
            else
            {
                if (bestValue >= alphaOrig)
                {
                    this.SetHashEntry(board, hashBoard, depth, bestValue, NodeType.LowerBound);
                }
                else
                {
                    this.SetHashEntry(board, hashBoard, depth, bestValue, NodeType.Exact);
                }
            }

            return bestValue;
        }

        private void SetHashEntry(Board board, long hashBoard, int depth, float value, NodeType nodeType)
        {
            if (TranspositionTable.ContainsKey(hashBoard))
            {
                var hashEntry = TranspositionTable[hashBoard];
                if ((depth >= hashEntry.Depth && value >= hashEntry.Score))
                {
                    TranspositionTable[hashBoard] = new HashEntry(hashBoard, depth, value, nodeType);
                }
            }
            else
            {
                TranspositionTable[hashBoard] = new HashEntry(hashBoard, depth, value, nodeType);
            }
        }

        private int MaterialDifference(Board board)
        {
            return Math.Abs(board.MaterialWhite - board.MaterialBlack);
        }

        public float Evaluate(
            Board board,
            int turn,
            int oppositeTurn,
            bool maxOrMin,
            int totalMobility,
            int keepQuiescenceSeach,
            float alpha,
            float beta,
            Guid idPieceToCompare,
            bool queening)
        {
            var isCheckOpposite = this.IsCheck(board, oppositeTurn, oppositeTurn == 1);

            //if ((isCheckOpposite || queening) && keepQuiescenceSeach >= 0)
            //{
            //    return -this.NegaMax(board, 2, -beta, -alpha, turn, !maxOrMin, idPieceToCompare, keepQuiescenceSeach - 1);
            //}

            var h = this.Heuristic(board, turn, totalMobility, turn == 1, isCheckOpposite, idPieceToCompare);
            return h;
        }

        public float Heuristic(Board board, int turn, int mobility, bool turnEqualOne, bool isCheckOpposite, Guid idPieceToCompare, int materialDifference = -1)
        {
            var candidatesOpposite = this.GetLegalCandidates(board, !turnEqualOne, isCheckOpposite, 0).ToList();

            float value = 0;
            int[] columnsAmountPawns = ColumnsAmountPawnsDefaultValue;
            int[] columnsAmountPawnsOpposite = ColumnsAmountPawnsOppositeValue;

            value += this.VariationMaterialEvaluation(board, turnEqualOne, materialDifference)
                + this.VariationCastleEvaluation(board, turnEqualOne)
                + mobility - candidatesOpposite.Count
                + this.VariationPositionalEvaluation(board, turn, turnEqualOne, columnsAmountPawns, columnsAmountPawnsOpposite, isCheckOpposite, PositionalEvaluationConsideration)
                + this.VariationPawnEvaluation(columnsAmountPawns, columnsAmountPawnsOpposite)
                + this.SamePieceMoveRepetitionEvaluation(turnEqualOne, idPieceToCompare);

            return value;
        }

        private float SamePieceMoveRepetitionEvaluation(bool thisBandEqualOne, Guid idPieceToCompare)
        {
            if (!idPieceToCompare.Equals(Guid.Empty))
            {
                var valueOfPieceToCompare = 0;
                if (thisBandEqualOne && idPieceToCompare.Equals(this.PreviousWhitePieceMoved))
                {
                    valueOfPieceToCompare -= ValueForSamePieceMoved;
                }
                else if (idPieceToCompare.Equals(this.PreviousBlackPieceMoved))
                {
                    valueOfPieceToCompare -= ValueForSamePieceMoved;
                }

                return valueOfPieceToCompare;
            }

            return 0;
        }

        private float VariationPawnEvaluation(int[] columnsAmountPawns, int[] columnsAmountPawnsOpposite)
        {
            return this.PawnEvaluation(columnsAmountPawns) - this.PawnEvaluation(columnsAmountPawnsOpposite);
        }

        private float VariationPositionalEvaluation(Board board, int turn, bool turnEqualsOne, int[] columnsAmountPawns, int[] columnsAmountPawnsOpposite, bool isCheckOpposite, bool substractOppositePositional = false)
        {
            var squares = (turnEqualsOne ? board.WhiteSquaresWithPieces : board.BlackSquaresWithPieces).Values;
            var squaresOpposites = (turnEqualsOne ? board.BlackSquaresWithPieces : board.WhiteSquaresWithPieces).Values;

            var kingSquareOpposite = turnEqualsOne ? board.BlackKingPosition : board.WhiteKingPosition;
            var kingSquare = turnEqualsOne ? board.WhiteKingPosition : board.BlackKingPosition;

            var posMax = this.PositionalEvaluation(board, turn, turnEqualsOne, squares, columnsAmountPawns, kingSquareOpposite, null, false);
            var posMin = this.PositionalEvaluation(board, -turn, !turnEqualsOne, squaresOpposites, columnsAmountPawnsOpposite, kingSquare, null, isCheckOpposite);

            return (substractOppositePositional 
                 ? (posMax - posMin)
                 : posMax);
        }

        private float PawnEvaluation(int[] doubledPawns)
        {
            float value = 0;

            var isolatedPawns = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i > 0 && i < 7)
                {
                    if (doubledPawns[i - 1] == 0 && doubledPawns[i + 1] == 0)
                    {
                        isolatedPawns++;
                    }
                }

                if (doubledPawns[i] > 1)
                {
                    value -= doubledPawns[i] * 2;
                }
            }

            value -= isolatedPawns ^ 2;
            return value;
        }

        private float PositionalEvaluation(
            Board board,
            int turn,
            bool turnEqualsOne,
            IEnumerable<Square> squares,
            int[] doubledPawns,
            Square kingSquareOpposite,
            List<Operator> candidates,
            bool isCheck)
        {
            float value = 0;

            // positional values
            foreach (var square in squares)
            {
                //int[] defendedBy;
                //int countAttackers, countDefenders;
                //var attackingValues = this.GetAttackingAndDefendingValues(
                //    board,
                //    square,
                //    turn,
                //    turnEqualsOne,
                //    out defendedBy,
                //    out countAttackers,
                //    out countDefenders);

                var piece = square.CurrentPiece;
                var currentPieceValue = piece.Value;

                if (currentPieceValue == PieceValue.Pawn)
                {
                    if (this.GamePhase == GamePhase.EndGame)
                    {
                        if (this.FullfillsSquareRule(board, square.Column, square.Row, turnEqualsOne))
                        {
                            value -= ValueWhenOppositeKingCatchYouOverQueening;
                        }
                    }

                    if (this.IsPassedPawn(board, square))
                    {
                        value += ((turnEqualsOne ? square.Row : 7 - square.Row) * ValueFactorForAdditionRowToPassedPawn * 2);
                    }

                    doubledPawns[square.Column]++;

                    value += turnEqualsOne
                                 ? this.WhitePawnMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackPawnMatrix.GetValue(square.Column, square.Row);

                    if (square.Column == 0 || square.Column == 7)
                    {
                        value -= (float)(value * DiscountValueForBorderPawns);
                    }

                    var row = turnEqualsOne ? square.Row + 1 : square.Row - 1;

                    if (row > -1 && row < 8)
                    {
                        if (square.Column > 1)
                        {
                            Square squareLeft = board.GetSquare(square.Column - 1, row);

                            if (squareLeft.CurrentPiece != null && squareLeft.CurrentPiece.Color == PieceColor.White)
                            {
                                value += (float)(squareLeft.CurrentPiece.Value - PieceValue.Pawn) / 10;
                            }
                        }

                        if (square.Column < 7)
                        {
                            Square squareRight = board.GetSquare(square.Column + 1, row);

                            if (squareRight.CurrentPiece != null && squareRight.CurrentPiece.Color == PieceColor.White)
                            {
                                value += (float)(squareRight.CurrentPiece.Value - PieceValue.Pawn) / 10;
                            }
                        }
                    }
                }

                if (currentPieceValue == PieceValue.King)
                {
                    value += turnEqualsOne
                                 ? this.WhiteKingMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackKingMatrix.GetValue(square.Column, square.Row);
                }

                if (currentPieceValue == PieceValue.Queen)
                {
                    value += turnEqualsOne
                                 ? this.WhiteQueenMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackQueenMatrix.GetValue(square.Column, square.Row);

                    if (square.Row == kingSquareOpposite.Row)
                    {
                        value += ValueForQueenInSameRowThanOppositeKing;
                    }

                    if (square.Column == kingSquareOpposite.Column)
                    {
                        value += ValueForQueenInSameColumnThanOppositeKing;
                    }

                    if (square.GetUnitarianVector().Equals(kingSquareOpposite.GetUnitarianVector()))
                    {
                        value += ValueForQueenInSameDiagonalThanOppositeKing;
                    }

                    if (square.CurrentPiece.Developed)
                    {
                        value += ValueForDevelopingQueen;
                    }
                }

                if (currentPieceValue == PieceValue.Rock)
                {
                    if ((piece.Color == PieceColor.White && square.Row == 6)
                        || (piece.Color == PieceColor.Black && square.Row == 1))
                    {
                        value += ValueForRockInPenultimRow;
                    }

                    value += turnEqualsOne
                                 ? this.WhiteRockMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackRockMatrix.GetValue(square.Column, square.Row);

                    if (square.Row == kingSquareOpposite.Row)
                    {
                        value += ValueForRockInSameRowThanOppositeKing;
                    }

                    if (square.Column == kingSquareOpposite.Column)
                    {
                        value += ValueForRockInSameColumnThanOppositeKing;
                    }
                }

                if (currentPieceValue == PieceValue.Bishop)
                {
                    value += turnEqualsOne
                                 ? this.WhiteBishopMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackBishopMatrix.GetValue(square.Column, square.Row);

                    if (square.GetUnitarianVector().Equals(kingSquareOpposite.GetUnitarianVector()))
                    {
                        value += ValueForBishopInSameDiagonalThanOppositeKing;
                    }

                    if (square.CurrentPiece.Developed)
                    {
                        value += ValueForDevelopingBishop;
                    }
                }

                if (currentPieceValue == PieceValue.Knight)
                {
                    value += turnEqualsOne
                                 ? this.WhiteKnightMatrix.GetValue(square.Column, square.Row)
                                 : this.BlackKnightMatrix.GetValue(square.Column, square.Row);

                    if (square.CurrentPiece.Developed)
                    {
                        value += ValueForDevelopingKnight;
                    }

                    if (turnEqualsOne)
                    {
                        if (square.Row <= 5)
                        {
                            var nextRowPieceValue = board.GetSquare(square.Column, square.Row + 1);
                            if (nextRowPieceValue.CurrentPiece != null
                                && nextRowPieceValue.CurrentPiece.Value == PieceValue.Pawn
                                && nextRowPieceValue.CurrentPiece.Color.Equals(PieceColor.Black))
                            {
                                value += ValueForKnightBlockingPawn;
                            }
                        }
                    }
                    else
                    {
                        if (square.Row >= 2)
                        {
                            var nextRowPieceValue = board.GetSquare(square.Column, square.Row - 1);
                            if (nextRowPieceValue.CurrentPiece != null
                                && nextRowPieceValue.CurrentPiece.Value == PieceValue.Pawn
                                && nextRowPieceValue.CurrentPiece.Color.Equals(PieceColor.White))
                            {
                                value += ValueForKnightBlockingPawn;
                            }
                        }
                    }
                }

                //var pieceValue = (int)piece.Value;

                //if (countDefenders == 0)
                //{
                //    value -= ReductionFactorForAttackerPiece;
                //}

                //if (countAttackers > countDefenders)
                //{
                //    value -= ValueForNumAttackersGreaterThanNumDefenders;
                //}
            }

            if (isCheck)
            {
                value -= 10;
            }

            return value;
        }

        private float VariationCastleEvaluation(Board board, bool turnEqualsOne)
        {
            if (turnEqualsOne)
            {
                return (board.WhiteHasCastled ? ValueCastleEvaluation : 0)
                       - (board.BlackHasCastled ? ValueCastleEvaluation : 0);
            }

            return (board.BlackHasCastled ? ValueCastleEvaluation : 0)
                       - (board.WhiteHasCastled ? ValueCastleEvaluation : 0);
        }

        private float VariationMaterialEvaluation(Board board, bool turnEqualsOne, int materialDifference)
        {
            float value = 0;

            if (materialDifference != -1)
            {
                return value;
            }

            if (turnEqualsOne)
            {
                value = board.MaterialWhite - board.MaterialBlack;
            }
            else
            {
                value = board.MaterialBlack - board.MaterialWhite;
            }

            return value;
        }

        private bool FullfillsSquareRule(Board board, int column, int row, bool turnEqualsOne)
        {
            if (turnEqualsOne)
            {
                var king = board.BlackKingPosition;

                var columnToQueening = 7 - column;
                var columnDistanceToOppositeKing = Math.Abs(king.Column - column);

                return columnDistanceToOppositeKing <= columnToQueening && king.Row >= row;
            }
            else
            {
                var king = board.WhiteKingPosition;

                var columnToQueening = column - 7;
                var columnDistanceToOppositeKing = Math.Abs(king.Column - column);

                return columnDistanceToOppositeKing <= columnToQueening && king.Row <= row;
            }
        }

        private bool IsPassedPawn(Board board, Square square)
        {
            var isWhite = square.CurrentPiece.Color == PieceColor.White;
            var column = square.Column;
            var rowOppositePawn = this.IsThereKindOfPieceInColumnReturnRow(board, isWhite, column);
            return isWhite ? square.Row > rowOppositePawn : square.Row < rowOppositePawn;
        }

        private int IsThereKindOfPieceInColumnReturnRow(Board board, bool isWhite, int column)
        {
            if (isWhite)
            {
                for (int i = 7; i >= 0; i--)
                {
                    var piece = board.GetSquare(column, i).CurrentPiece;
                    if (piece != null && piece.Value == PieceValue.Pawn && piece.Color == PieceColor.Black)
                    {
                        return i;
                    }
                }
            }
            else
            {
                var type = typeof(WhitePawn);
                for (int i = 0; i < 8; i++)
                {
                    if (board.GetSquare(column, i).CurrentPiece != null
                        && board.GetSquare(column, i).CurrentPiece.GetType() == type)
                    {
                        return i;
                    }
                }
            }

            return isWhite ? -1 : 8;
        }
    }
}
