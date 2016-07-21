namespace Chessty.Movement
{
    using Chessty.Enumeration;

    public class PawnMove : Move
    {
        public PawnMoveType Type { get; set; }
        public PawnMove(PawnMoveType type, int columns, int rows) : base(columns, rows)
        {
            this.Type = type;
            this.MoveType = 1;
        }
    }
}
