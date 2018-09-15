namespace Chessty.AI
{
    public interface IDataService
    {
        short[] GetPawnTableWhite();
        short[] GetKnightTableWhite();
        short[] GetBishopTableWhite();
        short[] GetRockTableWhite();
        short[] GetQueenTableWhite();
        short[] GetPawnTableBlack();
        short[] GetKnightTableBlack();
        short[] GetBishopTableBlack();
        short[] GetRockTableBlack();
        short[] GetQueenTableBlack();
    }
}
