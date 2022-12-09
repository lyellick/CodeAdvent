namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 8: Treetop Tree House
    /// </summary>
    public class Day08
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 8);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var forest = _puzzle.ToEnumerable((row) => row.Select(c => c - '0').ToArray()).ToArray();

            var horizontal = forest;

            var vertical = forest.ToPivot();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}