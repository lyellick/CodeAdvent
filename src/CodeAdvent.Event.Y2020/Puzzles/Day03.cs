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
            var rows = _puzzle.ToEnumerable((line) => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();

            int trees = SimiulateTrajectory(rows, (3, 1));

            Assert.That(trees, Is.EqualTo(268));
        }

        [Test]
        public void Part2()
        {
            var slopes = new (int right, int down)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            var rows = _puzzle.ToEnumerable((line) => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();

            var trees = slopes.Select(slope => SimiulateTrajectory(rows, slope)).ToArray();

            var total = (Int64)trees[0] * (Int64)trees[1] * (Int64)trees[2] * (Int64)trees[3] * (Int64)trees[4];

            Assert.That(total, Is.EqualTo(3093068400));
        }

        private int SimiulateTrajectory(int[][] rows, (int right, int down) slope)
        {
            int trees = 0;
            int row = 0;
            int col = 0;

            do
            {
                if (col >= rows[row].Length)
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

                row += slope.down;
                col += slope.right;

            } while (row < rows.Length);

            return trees;
        }
    }
}