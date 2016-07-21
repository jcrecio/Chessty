namespace Chessty
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Chessty.Abstract;
    using Chessty.AI;
    using Chessty.Enumeration;
    using Chessty.Structure;

    [DataContract]
    public class Game
    {
        [DataMember]
        public Board Board { get; set; }
        [DataMember]
        public AIManager AiManager { get; set; }
        [DataMember]
        public int Turn { get; set; }
        [DataMember]
        public Sequence CurrentPlySequence { get; set; }
        [DataMember]
        public Sequence PreviousPlySequence { get; set; }
        [DataMember]
        public Dictionary<int, Tuple<Operator, Square, Square, Piece, bool[], Piece, bool, Tuple<bool, GamePhase, long>>> UndoTable { get; set; }
        [DataMember]
        public Dictionary<string, Dictionary<string, Sequence>> Sequences { get; set; }
        [DataMember]
        public Matrix.Matrix WhitePawnMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackPawnMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix WhiteBishopMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackBishopMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix WhiteRockMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackRockMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix WhiteKnightMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackKnightMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix WhiteQueenMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackQueenMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix WhiteKingMatrix { get; set; }
        [DataMember]
        public Matrix.Matrix BlackKingMatrix { get; set; }

        public Game(IEnumerable<Sequence> openings = null)
        {
            this.Turn = 1; // White
            this.AiManager = new AIManager();
            this.Board = new Board();


            CurrentPlySequence = new Sequence();
            PreviousPlySequence = new Sequence();
            UndoTable = new Dictionary<int, Tuple<Operator, Square, Square, Piece, bool[], Piece, bool, Tuple<bool, GamePhase, long>>>();

            this.AiManager.GamePhase = GamePhase.Opening;
            Sequences = new Dictionary<string, Dictionary<string, Sequence>>();
            if (openings != null)
            {
                this.LoadOpenings(openings);   
            }
        }

        private void LoadOpenings(IEnumerable<Sequence> openings)
        {
            foreach (var sequence in openings)
            {
                this.AddOpening(sequence);
            }
        }

        public void AddOpening(Sequence sequence)
        {
            var subsequence = new Sequence();
            var key = sequence.ToString();

            var dic = new Dictionary<string, Sequence>();

            var plies = sequence.GetPlies();
            foreach (var ply in plies)
            {
                subsequence.Push(ply.Clone());
                var copySubsequence = subsequence.Clone();

                dic.Add(copySubsequence.ToString(), subsequence);
            }

            this.Sequences.Add(key, dic);
        }

        public Operator Search(Board board, int turn, int depth)
        {
            return this.AiManager.Search(board, turn, depth);
        }
    }
}
