﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chessty.Pieces
{
    using Chessty;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Abstract;
    using Chessty.Contracts;
    using Chessty.Structure;

    public class Queen : Piece, INotPawnOrKingPiece
    {
        private static List<Move> listMoves = new List<Move>{
                new Move(1, 1),
                new Move(2, 2),
                new Move(3, 3),
                new Move(4, 4),
                new Move(5, 5),
                new Move(6, 6),
                new Move(7, 7),
                new Move(-1, 1),
                new Move(-2, 2),
                new Move(-3, 3),
                new Move(-4, 4),
                new Move(-5, 5),
                new Move(-6, 6),
                new Move(-7, 7),
                new Move(1, -1),
                new Move(2, -2),
                new Move(3, -3),
                new Move(4, -4),
                new Move(5, -5),
                new Move(6, -6),
                new Move(7, -7),
                new Move(-1, -1),
                new Move(-2, -2),
                new Move(-3, -3),
                new Move(-4, -4),
                new Move(-5, -5),
                new Move(-6, -6),
                new Move(-7, -7),
                new Move(0, 1),
                new Move(0, 2),
                new Move(0, 3),
                new Move(0, 4),
                new Move(0, 5),
                new Move(0, 6),
                new Move(0, 7),
                new Move(1, 0),
                new Move(2, 0),
                new Move(3, 0),
                new Move(4, 0),
                new Move(5, 0),
                new Move(6, 0),
                new Move(7, 0),
                new Move(0, -1),
                new Move(0, -2),
                new Move(0, -3),
                new Move(0, -4),
                new Move(0, -5),
                new Move(0, -6),
                new Move(0, -7),
                new Move(-1, 0),
                new Move(-2, 0),
                new Move(-3, 0),
                new Move(-4, 0),
                new Move(-5, 0),
                new Move(-6, 0),
                new Move(-7, 0),
            };

        public bool Developed { get; set; }

        public Queen(PieceColor color, Square initialSquare)
            : base(color, PieceValue.Queen, initialSquare)
        {
            Developed = false;
        }

        public override List<Move> GetMoves()
        {
            return listMoves;
        }

        public override void OnAfterMove()
        {
            Developed = true;
        }

        public override void Undo(params bool[] previousValues)
        {
            Developed = previousValues[0];
        }

        public override string ToString()
        {
            return this.Color.Equals(PieceColor.White) ? "_wq_|" : "_bq_|";
        }

        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.Queen;
            }
        }
    }
}
