namespace Chessty.Hash
{
    using Chessty.Enumeration;

    public class HashEntry
    {

        public HashEntry(long hashValue, int depth, float score, NodeType nodeType)
        {
            this.HashValue = hashValue;
            this.Depth = depth;
            this.Score = score;
            this.NodeType = nodeType;
        }

        public long HashValue { get; set; }
        public int Depth { get; set; }
        public float Score { get; set; }
        public NodeType NodeType { get; set; }
    }
}
