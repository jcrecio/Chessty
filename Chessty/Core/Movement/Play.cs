using Chessty.Structure;

namespace Chessty.Movement
{
    public class Play
    {
        public Board Board { get; set; }
        public Move Move { get; set; }
        public Square Square { get; set; }
        public bool IsInCheck { get; set; }
    }
}
