using System.Diagnostics.Metrics;

namespace CodeAdvent.Event.Y2021.Puzzles
{
    /// <summary>
    /// Day 1: Sonar Sweep
    /// </summary>
    public class Day01
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2021, 1);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var measurements = _puzzle.ToEnumerable((measurement) => int.Parse(measurement)).ToArray();

            var count = measurements.Select((n, i) => i != 0 ? n > measurements[i - 1] ? 1 : 0 : 0).Sum();

            Assert.That(count, Is.EqualTo(1553));
        }

        [Test]
        public void Part2()
        {
            var measurements = _puzzle.ToEnumerable((measurement) => int.Parse(measurement)).ToArray();

            var windows = measurements.Select((n, i) => i < measurements.Length - 2 ? new int[] { n, measurements[i + 1], measurements[i + 2] } : Array.Empty<int>()).ToArray();

            var count = windows.Select((n, i) => i != 0 ? n.Sum() > windows[i - 1].Sum() ? 1 : 0 : 0).Sum();

            Assert.That(count, Is.EqualTo(1597));
        }
    }
}