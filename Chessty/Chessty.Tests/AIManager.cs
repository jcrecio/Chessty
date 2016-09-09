using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Chessty.Tests
{

    [TestClass]
    public class AIManager
    {
        [TestClass]
        public class GetLegalCandidates
        {
            [TestMethod]
            public void ShouldReturn_MoveKnight_Up2_Right1_InInitialPosition()
            {
                var game = new Game();
                var board = game.Board;
                var manager = game.AiManager;

                var candidates = manager.GetLegalCandidates(board, true, false, 1).ToList();

                Assert.IsTrue(candidates.Any(c => c.Move.Row == 2 && c.Move.Column == 1 && c.Type == 1));
            }

            [TestMethod]
            public void ShouldReturn_MoveKnight_Up2_Left1_InInitialPosition()
            {
                var game = new Game();
                var board = game.Board;
                var manager = game.AiManager;

                var candidates = manager.GetLegalCandidates(board, true, false, 1).ToList();

                Assert.IsTrue(candidates.Any(c => c.Move.Row == 2 && c.Move.Column == -1 && c.Type == 1));
            }
        }
    }
}
