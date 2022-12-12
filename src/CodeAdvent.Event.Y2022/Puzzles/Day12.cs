namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 12: Unknown
    /// </summary>
    public class Day12
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 12);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}