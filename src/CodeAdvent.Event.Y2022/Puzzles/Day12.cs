namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 12: Hill Climbing Algorithm
    /// </summary>
    public class Day12
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 12);

            _puzzle.Input = _puzzle.Input.Replace("S", "a").Replace("E", "z");

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int steps = 0;

            (int row, int col) current = (20, 0), end = (20, 88);

            var map = _puzzle.ToEnumerable((line) => line.Select(c => c - 97).ToArray()).ToArray();

            do
            {
                var neighbors = map.GetNeighbors(current.row, current.col);

                foreach (var neighbor in neighbors)
                {
                    var prev = map[current.row][current.col];
                    var next = map[neighbor.row][neighbor.col];

                    if (neighbor != current && (next == prev + 1 || prev == next))
                        // Split off to branching paths?
                        current = neighbor; steps++;
                }

            } while (current != end);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private double GetDistance((int row, int col) start, (int row, int col) end) => Math.Sqrt(Math.Pow((end.col - start.col), 2) + Math.Pow((end.row - start.row), 2));
    }
}