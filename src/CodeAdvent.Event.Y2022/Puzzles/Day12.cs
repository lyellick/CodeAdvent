using System.Drawing;

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
            (int row, int col) begining = (20, 0), end = (20, 88);

            var map = _puzzle
                .ToEnumerable((line) => line.Select(c => c - 97))
                .Select((cols, row) => cols
                    .Select((elevation, col) => new Cell(row, col, elevation, end)).ToArray())
                .ToArray();

            Cell start = map[begining.row][begining.col], finish = map[end.row][end.col];

            start.Parent = 0;

            Queue<Cell> queue = new(new[] { start });

            int steps = 0;

            do
            {
                var current = queue.Dequeue();

                var neighbors = current.GetNeighbors(map);

                var available = neighbors
                    .Where(cell => cell.Parent == null)
                    .Where(cell => cell.Elevation <= current.Elevation + 1)
                    .ToArray();

                if (current.Id == finish.Id)
                    steps = current.Parent.Value;

                foreach (var cell in available)
                {
                    cell.Parent = current.Parent + 1;
                    queue.Enqueue(cell);
                }
            } while (steps == 0);

            Assert.That(steps, Is.EqualTo(380));
        }

        [Test]
        public void Part2()
        {
            (int row, int col) begining = (20, 88);

            var map = _puzzle
                .ToEnumerable((line) => line.Select(c => c - 97))
                .Select((cols, row) => cols
                    .Select((elevation, col) => new Cell(row, col, elevation)).ToArray())
                .ToArray();

            Cell start = map[begining.row][begining.col];

            start.Parent = 0;

            Queue<Cell> queue = new(new[] { start });

            int steps = 0;

            do
            {
                var current = queue.Dequeue();

                var neighbors = current.GetNeighbors(map);

                var available = neighbors
                    .Where(cell => cell.Parent == null)
                    .Where(cell => cell.Elevation >= current.Elevation - 1)
                    .ToArray();

                if (current.Elevation == 0)
                    steps = current.Parent.Value;

                foreach (var cell in available)
                {
                    cell.Parent = current.Parent + 1;
                    queue.Enqueue(cell);
                }
            } while (steps == 0);

            Assert.That(steps, Is.EqualTo(375));
        }
    }

    public class Cell
    {
        public Guid Id { get; set; }

        public int? Parent { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public int Elevation { get; set; }

        public double Distance { get; set; }

        public Cell(int row, int col, int elevation, (int row, int col)? end = null, int? seen = null)
        {
            Id = Guid.NewGuid();
            Parent = seen;
            Row = row;
            Col = col;
            Elevation = elevation;
            if (end != null)
                Distance = Math.Sqrt(Math.Pow((end.Value.col - col), 2) + Math.Pow((end.Value.row - row), 2));
        }

        public Cell[] GetNeighbors(Cell[][] map)
        {
            Cell[] neighbors = Array.Empty<Cell>();

            List<(int row, int col)> locations = new() { (Row + 1, Col), (Row, Col + 1), (Row - 1, Col), (Row, Col - 1) };

            neighbors = locations
                .Where(location => map.IsWithinBounds(location.row, location.col))
                .Select(location => map[location.row][location.col])
                .ToArray();

            return neighbors;
        }
    }
}