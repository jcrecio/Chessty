namespace Chessty.Search
{
    public class SearchParameter
    {
        public int TotalMobility { get; set; }
        public int PreviousWhiteMaterial { get; set; }
        public int PreviousBlackMaterial { get; set; }
        public int StopSearch { get; set; }
        public int Turn { get; set; }
        public int TerminalComing { get; set; }
        public bool MaxOrMin { get; set; }
    }
}
