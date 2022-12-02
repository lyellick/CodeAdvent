namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// </summary>
    public class Day02
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 2);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            /**
             * A for Rock, B for Paper, and C for Scissors
             * X for Rock, Y for Paper, and Z for Scissors
             */
            var wins = new Dictionary<char, char> { { 'A', 'Y' }, { 'C', 'X' }, { 'B', 'Z' } };
            var looses = new Dictionary<char, char> { { 'A', 'Z' }, { 'C', 'Y' }, { 'B', 'X' } };
            var conversions = new Dictionary<char, char> { { 'Z', 'C' }, { 'Y', 'B' }, { 'X', 'A' } };
            var scores = new Dictionary<char, int> { { 'Z', 3 }, { 'Y', 2 }, { 'X', 1 } };

            var rounds = _puzzle.ToEnumerable<(char opponent, char counter, int outcome)>((round) => (round[0], round[2], 0)).ToArray();

            for (int round = 0; round < rounds.Length; round++)
            {
                char opponent = rounds[round].opponent, counter = rounds[round].counter;

                // win
                if (wins[opponent] == counter)
                    rounds[round].outcome = scores[counter] + 6;

                // draw
                if (opponent == conversions[counter])
                    rounds[round].outcome = scores[counter] + 3;

                // loose
                if (looses[opponent] == counter)
                    rounds[round].outcome = scores[counter] + 0;
            }

            var points = rounds.Sum(round => round.outcome);

            Assert.That(points, Is.EqualTo(12855));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}