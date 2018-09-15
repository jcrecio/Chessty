namespace Chessty
{
    public class ChessConfig
    {
        public int MiddleGameMaterial = 21700;
        public float Minimum = -20000;
        public float Maximum = 20000;
        public float IntValueKing = 20000;
        public float ValueForDevelopingKnight = 10;
        public float ValueForDevelopingBishop = 10;
        public float ValueForDevelopingQueen = 4;
        public float ValueForNumAttackersGreaterThanNumDefenders = 0.25f;
        public float ReductionFactorForAttackerPiece = 0.25f;
        public float ValueForKnightBlockingPawn = 1;
        public float ValueForBishopInSameDiagonalThanOppositeKing = 0.25f;
        public float ValueForRockInSameColumnThanOppositeKing = 0.25f;
        public float ValueForRockInSameRowThanOppositeKing = 0.25f;
        public float ValueForRockInPenultimRow = 1;
        public float ValueForQueenInSameDiagonalThanOppositeKing = 0.25f;
        public float ValueForQueenInSameColumnThanOppositeKing = 0.25f;
        public float ValueForQueenInSameRowThanOppositeKing = 0.25f;
        public double DiscountValueForBorderPawns = 0.12;
        public float ValueWhenOppositeKingCatchYouOverQueening = 0.5f;
        public float ValueForPassedPawn = 1;
        public int KeepQuiescenceSeach = 3;
        public float ValueCastleEvaluation = 50;
        public float ValueFactorForAdditionRowToPassedPawn = 1;
        public int ValueForSamePieceMoved = 10;
        public int ReductionNullMoveDepth = 2;
        public bool MaxNode = true;
        public bool MinNode = false;
    }
}
