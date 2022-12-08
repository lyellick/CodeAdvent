namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 7: No Space Left On Device
    /// </summary>
    public class Day07
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 7);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            VirtualDirectory root = new("/");

            var history = _puzzle.ToEnumerable((line) => line.Split(" ")).ToArray();

            for (int i = 0; i < history.Length; i++)
            {

            }

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}