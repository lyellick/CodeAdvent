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
            int trees = 0;
            int row = 0;
            int col = 0;

            var rows = _puzzle.ToEnumerable((line) => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();

            do
            {
                if (col > rows[row].Length)
                {
                    do
                    {
                        var list = rows[row].ToList();
                        list.AddRange(rows[row]);

                        rows[row] = list.ToArray();
                    } while (!(col < rows[row].Length));
                }

                if (rows[row][col] == 1)
                    trees++;

                row++;
                col += 3;

            } while (row < rows.Length);

            Assert.That(trees, Is.EqualTo(268));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}