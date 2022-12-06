using System.Diagnostics.Metrics;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 6: Tuning Trouble
    /// </summary>
    public class Day06
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 6);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var buffer = _puzzle.Input.ToArray();

            var splits = buffer.Select((c, i) => i < buffer.Length - 3 ? new char[] { c, buffer[i + 1], buffer[i + 2], buffer[i + 3] } : Array.Empty<char>()).ToArray();

            var markers = splits.Where((split, i) => split.ToHashSet().Count == 4).ToArray();

            var processed = _puzzle.Input.Split(string.Join("", markers[0]))[0].Length + 4;

            Assert.That(processed, Is.EqualTo(1702));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}