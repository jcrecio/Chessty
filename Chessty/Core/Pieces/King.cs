using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chessty.Pieces
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Abstract;
    using Chessty.Structure;

    public abstract class King : Piece
    {
        private static readonly List<Move> ListMoves = new List<Move> {
                new KingMove(0, 1),
                new KingMove(1, 1),
                new KingMove(1, 0),
                new KingMove(1, -1),
                new KingMove(0, -1),
                new KingMove(-1, 1),
                new KingMove(-1, 0),
                new KingMove(-1, -1)
            };

        private static readonly List<Move> ListMovesWithCastle = new List<Move> {
                new KingMove(-2, 0, true),
                new KingMove(2, 0, true),
                new KingMove(0, 1),
                new KingMove(1, 1),
                new KingMove(1, 0),
                new KingMove(1, -1),
                new KingMove(0, -1),
                new KingMove(-1, 1),
                new KingMove(-1, 0),
                new KingMove(-1, -1),
            };

        public bool Castle { get; set; }

        protected King(PieceColor color, Square initialSquare) : base(color, PieceValue.King, initialSquare)
        {
            this.Castle = true;
        }

        public override List<Move> GetMoves()
        {
            return this.Castle ? ListMovesWithCastle : ListMoves;
        }

        public override void OnAfterMove()
        {
            this.Castle = false;
        }

        public override void Undo(params bool[] previousValues)
        {
            if (previousValues.Count() == 1)
            {
                this.Castle = previousValues[0];
            }
        }

        public override int GetMoveByPriority(Play play, Square squareTo, Func<Play, Square, bool> precondition)
        {
            if (!precondition(play, squareTo))
            {
                return Globals.MoveCannotBeDone;
            }
            
            if (!play.IsInCheck && play.Move.MoveType != Globals.MoveIsCapture && (play.Move as KingMove).Castling)
            {
                var canCastle = play.Square.CurrentPiece.Color == PieceColor.White
                    ? !play.Board.WhiteHasCastled
                    : !play.Board.BlackHasCastled && (play.Square.CurrentPiece as King).Castle;

                if (canCastle)
                {
                    var row = play.Square.CurrentPiece.Color == PieceColor.White ? 0 : BoardSize.MaxRowIndex;

                    if (this.IsCorrectCastle(play.Board, play.Move, row))
                    {
                        return Globals.MoveIsCastle;
                    }
                }
            }

            return base.GetMoveByPriority(play, squareTo);
        }

        private bool IsCorrectCastle(Board board, Move move, int row)
        {
            return (move.Column == 2
                        && board.GetSquare(5, row).CurrentPiece == null
                        && board.GetSquare(6, row).CurrentPiece == null)

                    || (move.Column == -2
                        && board.GetSquare(1, row).CurrentPiece == null
                        && board.GetSquare(2, row).CurrentPiece == null
                        && board.GetSquare(3, row).CurrentPiece == null);
        }
        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.King;
            }
        }
    }
}
