using System;
using System.Collections.Generic;
using chess_validator.Adapter;
using chess_validator.Core;
using chess_validator.Core.Exceptions;
using chess_validator.Core.Figures;
using chess_validator.Core.Movements;
using NUnit.Framework;

namespace chess_validator.Tests
{
    [TestFixture]
    public class StringPgnParserTest
    {
        [Test]
        public void ParsePgnMetadata()
        {
            const string content =
                "[Event \"3rd London Chess Classic\"]" +
                "[Site \"London ENG\"]" +
                "[Date \"2011.12.08\"]" +
                "[Round \"5\"]" +
                "[White \"Kramnik,V\"]" +
                "[Black \"Adams,Mi\"]" +
                "[Result \"*\"]";

            IPgnParser parser = new StringPgnParser(content);

            var expectedMatch = new Match(
                "3rd London Chess Classic",
                "London ENG",
                new DateTime(2011, 12, 8),
                5,
                new Player("V", "Kramnik"),
                new Player("Mi", "Adams"),
                new Result(GameState.STILL_PLAYING),
                new LinkedList<PairMovement>()
            );

            var actualMatch = parser.Parse();

            Assert.AreEqual(expectedMatch, actualMatch);
        }

        [Test]
        public void ParsePgnMetadataWhenSomeTagsAreWithUnknown()
        {
            const string content =
                "[Event \"?\"]" +
                "[Site \"?\"]" +
                "[Date \"2011.??.??\"]" +
                "[Round \"?\"]" +
                "[White \"Kramnik,V\"]" +
                "[Black \"Adams,Mi\"]" +
                "[Result \"*\"]";

            IPgnParser parser = new StringPgnParser(content);

            var expectedMatch = new Match(
                "",
                "",
                new DateTime(2011, 1, 1),
                1,
                new Player("V", "Kramnik"),
                new Player("Mi", "Adams"),
                new Result(GameState.STILL_PLAYING),
                new LinkedList<PairMovement>()
            );

            var actualMatch = parser.Parse();

            Assert.AreEqual(expectedMatch, actualMatch);
        }

        [Test]
        public void TryToParsePgnContentWithMissingTags()
        {
            try
            {
                const string content =
                    "[Date \"2011.??.??\"]" +
                    "[Round \"?\"]" +
                    "[White \"Kramnik,V\"]" +
                    "[Black \"Adams,Mi\"]" +
                    "[Result \"*\"]";

                new StringPgnParser(content).Parse();

                Assert.Fail("Exception should be thrown");
            }
            catch (MissingTagException e)
            {
            }
        }

        [Test]
        public void ParsePgnBasicMoves()
        {
            const string content =
                "[Date \"2011.12.08\"]" +
                "[Round \"5\"]" +
                "[White \"Kramnik,V\"]" +
                "[Black \"Adams,Mi\"]" +
                "[Result \"*\"]" +
                "1.a3 Nc3 2.Bh3 Ra2 3.Qe2 Kf1";

            var moves = new LinkedList<PairMovement>();

            moves.AddLast(new PairMovement(
                new Move(
                    new Pawn(new Coordinates('a', 2), Color.WHITE),
                    new Coordinates('a', 2),
                    new Coordinates('a', 3)
                ),
                new Move(
                    new Knight(new Coordinates('b', 1), Color.BLACK),
                    new Coordinates('b', 1),
                    new Coordinates('c', 3)
                ),
                1
            ));
            moves.AddLast(new PairMovement(
                new Move(
                    new Bishop(new Coordinates('f', 1), Color.WHITE),
                    new Coordinates('f', 1),
                    new Coordinates('h', 3)
                ),
                new Move(
                    new Rook(new Coordinates('a', 1), Color.BLACK),
                    new Coordinates('a', 1),
                    new Coordinates('a', 2)
                ),
                2
            ));
            moves.AddLast(new PairMovement(
                new Move(
                    new Queen(new Coordinates('d', 1), Color.WHITE),
                    new Coordinates('d', 1),
                    new Coordinates('e', 2)
                ),
                new Move(
                    new King(new Coordinates('e', 1), Color.BLACK),
                    new Coordinates('e', 1),
                    new Coordinates('f', 1)
                ),
                3
            ));

            IPgnParser parser = new StringPgnParser(content);

            var expectedMatch = new Match(
                "3rd London Chess Classic",
                "London ENG",
                new DateTime(2011, 12, 8),
                5,
                new Player("V", "Kramnik"),
                new Player("Mi", "Adams"),
                new Result(GameState.STILL_PLAYING),
                moves
            );

            var actualMatch = parser.Parse();

            Assert.AreEqual(expectedMatch, actualMatch);
        }
    }
}