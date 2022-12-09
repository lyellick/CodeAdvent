using System.Numerics;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 9: Rope Bridge
    /// </summary>
    public class Day09
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 9);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var simulation = _puzzle
                .ToEnumerable<(string direction, int steps)>(
                    @"(.+) (.+)", step => (step.Groups[1].Value, int.Parse(step.Groups[2].Value)))
                .ToArray();

            List<(int row, int col)> head = new() { (0, 0) }, tail = new() { (0, 0) };

            foreach (var step in simulation)
            {
                int steps = step.steps;

                do
                {
                    (int row, int col) = head[^1];

                    switch (step.direction)
                    {
                        case "U": head.Add((row - 1, col)); break;
                        case "D": head.Add((row + 1, col)); break;
                        case "L": head.Add((row, col - 1)); break;
                        case "R": head.Add((row, col + 1)); break;
                    }

                    (int row, int col) ch = head[^1], ct = tail[^1];

                    bool ul = (ct.row - 1, ct.col - 1) == ch,   // upper left
                         uc = (ct.row - 1, ct.col) == ch,       // upper center
                         ur = (ct.row - 1, ct.col + 1) == ch,   // upper right
                         cl = (ct.row, ct.col - 1) == ch,       // center left 
                         c = ct == ch,                          // center 
                         cr = (ct.row, ct.col + 1) == ch,       // center right
                         dl = (ct.row + 1, ct.col - 1) == ch,   // lower left
                         dc = (ct.row + 1, ct.col) == ch,       // lower center
                         dr = (ct.row + 1, ct.col + 1) == ch;   // lower right

                    bool isTouching = ul || uc || ur || cl || c || cr || dl || dc || dr;

                    if (!isTouching)
                        tail.Add((row, col));

                    steps--;
                } while (steps != 0);
            }

            var visited = tail.Distinct().Count();

            Assert.That(visited, Is.EqualTo(6081));
        }

        [Test]
        public void Part2()
        {
            var simulation = _puzzle
                .ToEnumerable<(string direction, int steps)>(
                    @"(.+) (.+)", step => (step.Groups[1].Value, int.Parse(step.Groups[2].Value)))
                .ToArray();

            Vector2[] length = new Vector2[10];

            HashSet<Vector2> path = new() { new Vector2() };

            foreach ((string direction, int steps) in simulation)
            {
                var next = new Vector2();

                switch (direction)
                {
                    case "U": next = new Vector2(0, 1); break;
                    case "D": next = new Vector2(0, -1); break;
                    case "L": next = new Vector2(-1, 0); break;
                    case "R": next = new Vector2(1, 0); break;
                }

                int step = steps;

                do
                {
                    length[0] += next;

                    for (int i = 1; i < length.Length; i++)
                    {
                        Vector2 head = length[i - 1], tail = length[i];

                        float rows = head.Y - tail.Y, columns = head.X - tail.X;

                        length[i] += (rows <= 1 && rows >= -1 && columns <= 1 && columns >= -1)
                            ? new()
                            : new Vector2(Math.Clamp(head.X - tail.X, -1, 1), Math.Clamp(head.Y - tail.Y, -1, 1));
                    }

                    path.Add(length[^1]);

                    step--;
                } while (step != 0);
            }

            var visited = path.Count;

            Assert.That(visited, Is.EqualTo(2487));
        }
    }
}