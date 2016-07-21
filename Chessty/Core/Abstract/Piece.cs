﻿namespace Chessty.Abstract
{
    using System;
    using System.Collections.Generic;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Structure;

    public abstract class Piece
    {
        public PieceColor Color { get; private set; }

        public PieceValue Value
        {
            get; 
            private set;
        }
        public bool Developed { get; set; }

        public Guid Id { get; set; }

        protected Piece(PieceColor color, PieceValue value, Square initialSquare)
        {
            this.Color = color;
            this.Value = value;
            initialSquare.CurrentPiece = this;

            this.Id = Guid.NewGuid();
        }

        public abstract List<Move> GetMoves();

        public virtual void OnAfterMove() {}
        public virtual void Undo(params bool[] previousValues) { }

        public string GetHashPiece()
        {
            return ((int)this.Value).ToString();
        }

        public abstract int PieceIdentifier { get; }
        
    }
}