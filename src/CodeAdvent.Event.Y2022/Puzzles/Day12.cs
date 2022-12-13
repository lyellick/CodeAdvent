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

            (int row, int col) s = (20, 0), e = (20, 88);

            var map = _puzzle
                .ToEnumerable((line) => line.Select(c => c - 97))
                .Select((cols, row) => cols.Select((elevation, col) => new Cell(row, col, elevation, e)).ToArray())
                .ToArray();

            Cell start = map[s.row][s.col], finish = map[e.row][e.col], current = start;

            List<Cell> path = new() { start }, queue = new();

            do
            {
                var neighbors = current.GetNeighbors(map);

                path.Add(neighbors.First());
            } while (start != finish);

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

            List<(int row, int col)> locations = new() { (Row + 1, Col), (Row, Col + 1), (Row - 1, Col), (Row, Col - 1) };

            neighbors = locations
                .Where(location => map.IsWithinBounds(location.row, location.col))
                .Select(location => map[location.row][location.col])
                .OrderBy(cell => cell.Distance).ThenBy(cell => cell.Elevation)
                .ToArray();

            return neighbors;
        }
    }
}