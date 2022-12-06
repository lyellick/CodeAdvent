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
            int size = 4;

            var buffer = _puzzle.Input.ToArray().SlidingSplit(size);

            var markers = buffer.Where((split, i) => split.ToHashSet().Count == size).ToArray();

            var processed = _puzzle.Input.Split(string.Join("", markers[0]))[0].Length + size;

            Assert.That(processed, Is.EqualTo(1702));
        }

        [Test]
        public void Part2()
        {
            var buffer = _puzzle.Input.ToArray().SlidingSplit(14);

            var markers = buffer.Where((split, i) => split.ToHashSet().Count == 14).ToArray();

            var processed = _puzzle.Input.Split(string.Join("", markers[0]))[0].Length + 14;

            Assert.Pass();
        }
    }
}