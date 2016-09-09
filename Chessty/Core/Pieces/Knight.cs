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
    using Chessty.Contracts;
    using Chessty.Structure;

    public class Knight : Piece, INotPawnOrKingPiece
    {
        static readonly List<Move> ListMoves = new List<Move>()
                       {
                           new Move(1, 2), new Move(2, 1), 
                           new Move(2, -1), new Move(1, -2), 
                           new Move(-1, -2), new Move(-2, -1),
                           new Move(-2, 1), new Move(-1, 2),
                       };

        public Knight(PieceColor color, Square initialSquare)
            : base(color, PieceValue.Knight, initialSquare)
        {
            Developed = false;
        }

        public override void OnAfterMove()
        {
            Developed = true;
        }

        public override void Undo(params bool[] previousValues)
        {
            Developed = previousValues[0];
        }

        public override List<Move> GetMoves()
        {
            return ListMoves;
        }

        public override int PieceIdentifier
        {
            get
            {
                return (int)PieceIdentifiers.Knight;
            }
        }

        public int GetMoveByPriorityTo(Square square)
        {
            var valuePieceInSquareTo = square.GetPieceValue();

            if (valuePieceInSquareTo == 0)
            {
                return Globals.MoveIsNormal;
            }

            if (valuePieceInSquareTo > PieceValue.Knight)
            {
                return 2 + valuePieceInSquareTo - PieceValue.Knight;
            }

            return Globals.MoveIsCapture;
        }
    }
}
