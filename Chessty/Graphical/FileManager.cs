namespace Graphical
{
    using System;

    using Chessty;

    public class FileManager
    {
        public Sequence ReadOpeningFromFile(string file)
        {
            var sequence = new Sequence();

            string[] pliesAsString = System.IO.File.ReadAllLines(file);

            foreach (string ply in pliesAsString)
            {
                if (!string.IsNullOrWhiteSpace(ply))
                {
                    var arrayPly = ply.Split(' ');
                    var c0 = Convert.ToInt16(arrayPly[0]);
                    var r0 = Convert.ToInt16(arrayPly[1]);
                    var c1 = Convert.ToInt16(arrayPly[2]);
                    var r1 = Convert.ToInt16(arrayPly[3]);
                    sequence.Push(new Ply(c0, r0, c1, r1));
                }
            }

            return sequence;
        }
    }
}
