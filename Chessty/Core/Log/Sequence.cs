namespace Chessty
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Sequence
    {
        private string sequenceString;
        private List<Ply> stack;

        public Sequence()
        {
            this.stack = new List<Ply>();
            this.sequenceString = string.Empty;
        }

        public Sequence(string sequenceString, List<Ply> stack)
        {
            this.stack = stack;
            this.sequenceString = sequenceString;
        }

        public void Push(Ply ply)
        {
            this.stack.Add(new Ply(ply.C0, ply.R0, ply.C1, ply.R1));
            this.sequenceString = this.GetSequenceString();
        }
        public Ply Pop()
        {
            var item = this.stack.LastOrDefault();

            this.stack.RemoveAt(this.stack.Count - 1);
            this.sequenceString = this.GetSequenceString();

            return item;
        }

        public List<Ply> GetPlies()
        {
            return stack.ToList();
        }

        public Ply GetFittingPly(Sequence sequence)
        {
            var pliesCount = sequence.GetPlies().Count;
            if (pliesCount >= this.stack.Count)
            {
                return null;
            }

            for (int i = 0; i < pliesCount; i++)
            {
                if (!sequence[i].Equals(this.stack[i]))
                {
                    return null;
                }
            }

            return this.stack[pliesCount];
        }

        public Ply this[int key]
        {
            get => this.stack[key];
        }

        public void CutTo(int count)
        {
            stack = stack.Take(count).ToList();
        }

        public override string ToString()
        {
            return this.sequenceString;
        }

        private string GetSequenceString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var stackItem in this.stack)
            {
                stringBuilder.Append(stackItem);
            }

            return stringBuilder.ToString();
        }

        public Sequence Clone()
        {
            return new Sequence(this.sequenceString, this.stack);
        }
    }
}