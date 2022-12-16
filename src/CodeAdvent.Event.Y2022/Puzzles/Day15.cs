using System.Drawing;

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

            var cols = new HashSet<int>();

            string pattern = @"Sensor at x=(.+), y=(.+): closest beacon is at x=(.+), y=(.+)";

            var scan = _puzzle.ToEnumerable<((int row, int col) sensor, (int row, int col) beacon)>(pattern, (line) => 
            {
                var groups = line.Groups.ToEnumerable<int>().ToArray();

                return ((groups[1], groups[0]), (groups[3], groups[2]));
            }).ToArray();

            var exlude = scan.Select(relation => relation.beacon).Distinct().Count(beacon => beacon.row == row);

            for (int i = 0; i < scan.Length; i++)
            {
                (int row, int col) sensor = scan[i].sensor, beacon = scan[i].beacon;

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
            Assert.Pass();
        }
    }
}