using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chessty
{
    public class Ply
    {
        public int C0 { get; set; }
        public int R0 { get; set; }
        public int C1 { get; set; }
        public int R1 { get; set; }

        public Ply()
        {
        }

        public Ply(int c0, int r0, int c1, int r1)
        {
            C0 = c0;
            R0 = r0;
            C1 = c1;
            R1 = r1;
        }

        public override string ToString()
        {
            return $"{C0}{R0}{C1}{R1}";
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Ply);
        }

        private bool Equals(Ply ply)
        {
            return ply.C0 == C0 && ply.C1 == C1 && ply.R0 == R0 && ply.R1 == R1;
        }

        public Ply Clone()
        {
            return new Ply(this.C0, this.R0, this.C1, this.R1);
        }
    }
}
