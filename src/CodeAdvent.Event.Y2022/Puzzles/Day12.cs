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

            (int row, int col) start = (20, 0), end = (20, 88);

            var map = _puzzle
                .ToEnumerable((line) => line.Select(c => c - 97))
                .Select((cols, row) => cols.Select((elevation, col) => new Cell(row, col, elevation, end)).ToArray())
                .ToArray();

            Cell current = map[start.row][start.col], finish = map[end.row][end.col];

            List<Cell> path = new() { current }, queue = new();

            do
            {
                var neighbors = current.GetNeighbors(map);


            } while (current != finish);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }

    public class Cell
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public int Elevation { get; set; }

        public double Distance { get; set; }

        public Cell(int row, int col, int elevation, (int row, int col) end)
        {
            Row = row;
            Col = col;
            Elevation = elevation;
            Distance = Math.Sqrt(Math.Pow((end.col - col), 2) + Math.Pow((end.row - row), 2));
        }

        public Cell[] GetNeighbors(Cell[][] map)
        {
            Cell[] neighbors = Array.Empty<Cell>();

            if (map.IsWithinBounds(Row, Col))
            {
                List<(int row, int col)> prospects = new() { (Row + 1, Col), (Row, Col + 1), (Row - 1, Col), (Row, Col - 1) };

                neighbors = prospects
                    .Where(prospective => map.IsWithinBounds(prospective.row, prospective.col))
                    .Select(prospect => map[prospect.row][prospect.col])
                    .OrderBy(prospect => prospect.Distance).ThenBy(prospect => prospect.Elevation)
                    .ToArray();
            }

            return neighbors;
        }
    }
}