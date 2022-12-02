namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 2: Rock Paper Scissors
    /// </summary>
    public class Day02
    {
        private CodeAdventPuzzle _puzzle;

        private readonly Dictionary<char, char> _wins = 
            new() { { 'A', 'Y' }, { 'C', 'X' }, { 'B', 'Z' } };
        
        private readonly Dictionary<char, char> _looses = 
            new() { { 'A', 'Z' }, { 'C', 'Y' }, { 'B', 'X' } };
        
        private readonly Dictionary<char, char> _draws = 
            new() { { 'Z', 'C' }, { 'Y', 'B' }, { 'X', 'A' } };
        
        private readonly Dictionary<char, int> _scores = 
            new() { { 'Z', 3 }, { 'Y', 2 }, { 'X', 1 } };

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 2);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var rounds = _puzzle.ToEnumerable<(char opponent, char counter, int outcome)>((round) => (round[0], round[2], 0)).ToArray();

            for (int round = 0; round < rounds.Length; round++)
            {
                char opponent = rounds[round].opponent, counter = rounds[round].counter;

                // win
                if (_wins[opponent] == counter)
                    rounds[round].outcome = _scores[counter] + 6;

                // draw
                if (opponent == _draws[counter])
                    rounds[round].outcome = _scores[counter] + 3;

                // loose
                if (_looses[opponent] == counter)
                    rounds[round].outcome = _scores[counter] + 0;
            }

            var points = rounds.Sum(round => round.outcome);

            Assert.That(points, Is.EqualTo(12855));
        }

        [Test]
        public void Part2()
        {
            var rounds = _puzzle.ToEnumerable<(char opponent, char counter, int outcome)>((round) => (round[0], round[2], 0)).ToArray();

            Assert.Pass();
        }
    }
}