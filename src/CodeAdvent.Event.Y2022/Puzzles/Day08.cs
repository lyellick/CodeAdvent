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

            var seen = 0;

            for (int row = 0; row < forest.Length; row++)
            {
                for (int col = 0; col < forest[0].Length; col++)
                {
                    var tree = forest[row][col];

                    bool vtb = Enumerable.Range(0, row).All(row => forest[row][col] < tree);
                    bool vbt = Enumerable.Range(row + 1, forest.Length - row - 1).All(row => forest[row][col] < tree);
                    bool hlr = Enumerable.Range(0, col).All(col => forest[row][col] < tree);
                    bool hrl = Enumerable.Range(col + 1, forest[0].Length - col - 1).All(col => forest[row][col] < tree);

                    if (vtb || vbt || hlr || hrl)
                        seen++;
                }
            }

            Assert.That(seen, Is.EqualTo(1679));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}