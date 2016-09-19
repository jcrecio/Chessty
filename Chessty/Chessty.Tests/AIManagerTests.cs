using Chessty.Abstract;
using Chessty.AI;
using Chessty.Enumeration;
using Chessty.Pieces.White;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Chessty.Movement;
using Chessty.Structure;

namespace Chessty.Tests
{

    [TestClass]
    public class AIManagerTests
    {
        [TestClass]
        public class GetLegalCandidates
        {
            Game game;
            Board board;
            AIManager manager;
            List<Operator> initialCandidates;

            [TestInitialize]
            public void Initialize()
            {
                game = new Game();
                board = game.Board;
                manager = game.AiManager;

                initialCandidates = manager.GetLegalCandidates(board, true, false, 1).ToList();
            }

            [TestMethod]
            public void ShouldReturn_MoveKnight_Up2_Right1_InInitialPosition()
            {
                Assert.IsTrue(initialCandidates.Any(c => c.Move.Row == 2
                    && c.Move.Column == 1
                    && c.Square.CurrentPiece is WhiteKnight
                    && c.Type == 1));
            }

            [TestMethod]
            public void ShouldReturn_MoveKnight_Up2_Left1_InInitialPosition()
            {
                Assert.IsTrue(initialCandidates.Any(c => c.Move.Row == 2
                    && c.Move.Column == -1
                    && c.Square.CurrentPiece is WhiteKnight
                    && c.Type == 1));
            }

            [TestMethod]
            public void ShouldReturn_MovePawn1Square_InInitialPosition()
            {
                Assert.IsTrue(initialCandidates.Any(c => c.Move.Row == 2 && c.Move.Column == 0 && c.Square.CurrentPiece is WhitePawn));
            }

            [TestMethod]
            public void ShouldReturn_MoveKing_Up1_AfterMovingPawnInRow1AndColumn4()
            {
                manager.MovePiece(board, new Square(4, 1), new Move(0, 1));

                initialCandidates = manager.GetLegalCandidates(board, true, false, 1).ToList();

                Assert.IsTrue(initialCandidates.Any(c => c.Move.Row == 1 && c.Move.Column == 0 && c.Square.CurrentPiece is WhiteKing));
            }

            [TestMethod]
            public void ShouldReturn_CastlingRight_AfterFreeinRightSideOfRow1()
            {
                manager.MovePiece(board, new Square(6, 0), new Move(-1, 2));
                manager.MovePiece(board, new Square(6, 1), new Move(0, 1));
                manager.MovePiece(board, new Square(5, 0), new Move(1, 1));

                initialCandidates = manager.GetLegalCandidates(board, true, false, 1).ToList();

                Assert.IsTrue(initialCandidates.Any(c => c.Move.Row == 0 
                && c.Move.Column == 2
                && c.Type == 4
                && c.Square.CurrentPiece is WhiteKing));
            }

        }
    }
}
