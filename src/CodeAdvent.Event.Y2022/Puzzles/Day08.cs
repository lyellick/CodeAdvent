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
            var horizontal = _puzzle.ToEnumerable((row) => row.Select(c => c - '0').ToArray()).ToArray();

            var vertical = horizontal.ToPivot();

            for (int row = 0; row < horizontal.Length; row++)
            {
                for (int col = 0; col < horizontal[row].Length; col++)
                {

                }
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