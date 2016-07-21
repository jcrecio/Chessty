namespace Chessty.Movement
{
    public class KingMove : Move
    {
        public bool Castling { get; private set; }

        public KingMove(int direction, bool castling = false)
            : base(direction)
        {
            this.Castling = castling;
        }
        
        public KingMove(int columns, int rows, bool castling = false)
            : base(columns, rows)
        {
            this.Castling = castling;
        }
    }
}
