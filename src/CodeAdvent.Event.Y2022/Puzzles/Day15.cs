namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 15: Beacon Exclusion Zone
    /// </summary>
    public class Day15
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 15);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var row = 2000000;

            var cols = new HashSet<long>();

            string pattern = @"Sensor at x=(.+), y=(.+): closest beacon is at x=(.+), y=(.+)";

            var scan = _puzzle.ToEnumerable<((long row, long col) sensor, (long row, long col) beacon)>(pattern, (line) => 
            {
                var groups = line.Groups.ToEnumerable<long>().ToArray();

                return ((groups[1], groups[0]), (groups[3], groups[2]));
            }).ToArray();

            var exlude = scan.Select(relation => relation.beacon).Distinct().Count(beacon => beacon.row == row);

            for (long i = 0; i < scan.Length; i++)
            {
                (long row, long col) sensor = scan[i].sensor, beacon = scan[i].beacon;

                var distance = sensor.ManhattanDistanceTo(beacon) - Math.Abs(row - sensor.row);

                var overlap = (distance * 2) + 1;

                if (overlap <= 0)
                    continue;
                    
                var start = sensor.col - distance;

                Array.ForEach(Enumerable.Range(Convert.ToInt32(start), Convert.ToInt32(overlap)).ToArray(), col => { cols.Add(col); });
            }

            var none = cols.Count - exlude;

            Assert.That(none, Is.EqualTo(5838453));
        }

        [Test]
        public void Part2()
        {
            long output = 0;

            var upperbound = 4000000;

            string pattern = @"Sensor at x=(.+), y=(.+): closest beacon is at x=(.+), y=(.+)";

            List<((long row, long col) sensor, (long row, long col) beacon, long weight)> evaluations = new();

            var scan = _puzzle.ToEnumerable<((long row, long col) sensor, (long row, long col) beacon)>(pattern, (line) =>
            {
                var groups = line.Groups.ToEnumerable<long>().ToArray();

                return ((groups[1], groups[0]), (groups[3], groups[2]));
            }).ToArray();

            foreach (var (sensor, beacon) in scan)
                evaluations.Add((sensor, beacon, sensor.ManhattanDistanceTo(beacon)));

            evaluations = evaluations.OrderBy(evaluation => evaluation.weight).ToList();

            for (int evaluation = 0; evaluation < evaluations.Count; evaluation++)
            {
                (long row, long col) sensor = evaluations[evaluation].sensor, beacon = evaluations[evaluation].beacon;

                var weight = evaluations[evaluation].weight;

                var expanse = Enumerable.Range(0, (int)weight + 1)
                    .SelectMany(modifier => GetPerimeter(sensor, weight, modifier))
                    .Where(evaluation => IsWithinBounds(evaluation, upperbound))
                    .ToArray();

                for (int entry = 0; entry < scan.Length; entry++)
                {
                    if (entry == evaluation)
                        continue;

                    if (!expanse.Any())
                        break;

                    var check = evaluations[entry];

                    expanse = expanse.Where(evaluation => check.sensor.ManhattanDistanceTo(evaluation) > check.weight).ToArray();
                }

                if (expanse.Any())
                {
                    output = (expanse[0].col * upperbound) + expanse[0].row;
                    break;
                }
            }

            Assert.That(output, Is.EqualTo(12413999391794));
        }

        private static bool IsWithinBounds((long row, long col) evaluation, int upperbound) => 
            evaluation.col >= 0 && evaluation.col <= upperbound && evaluation.row >= 0 && evaluation.row <= upperbound;

        private (long row, long col)[] GetPerimeter((long row, long col) sensor, long weight, long modifier)
        {
            return new (long row, long col)[]
            {
                (sensor.row - weight - 1 + modifier, sensor.col + modifier),
                (sensor.row + 1 + modifier, sensor.col - weight + modifier),
                (sensor.row - modifier, sensor.col - weight - 1 + modifier),
                (sensor.row + weight - modifier, sensor.col + 1 + modifier)
            };
        }
    }
}