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
            string pattern = @"Sensor at x=(.+), y=(.+): closest beacon is at x=(.+), y=(.+)";

            var scan = _puzzle.ToEnumerable<((int row, int col) sensor, (int row, int col) beacon)>(pattern, (line) => 
            {
                var groups = line.Groups.ToEnumerable<int>().ToArray();
                return ((groups[1], groups[0]), (groups[3], groups[2]));
            }).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}