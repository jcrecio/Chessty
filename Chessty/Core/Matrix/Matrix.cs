namespace Chessty.Matrix
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Matrix
    {
        private readonly short[] values;

        public Matrix(short[] values)
        {
            this.values = values;
        }

        public short GetValue(int column, int row)
        {
            return this.values[row * 8 + column];
        }

        public void LoadData(string file)
        {
            
        }
    }
}
