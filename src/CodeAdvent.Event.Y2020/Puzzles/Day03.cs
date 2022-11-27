namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 3: Toboggan Trajectory
    /// </summary>
    public class Day03
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 3);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var rows = _puzzle.ToEnumerable((line) => line.Select(c => c == '#' ? 1 : 0).ToArray());

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}