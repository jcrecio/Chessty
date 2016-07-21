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
                new KingMove(-2, 0),
                new KingMove(2, 0),
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

        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.King;
            }
        }
    }
}
