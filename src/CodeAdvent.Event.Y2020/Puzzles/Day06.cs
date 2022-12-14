using System.Linq;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 6: Custom Customs
    /// </summary>
    public class Day06
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 6);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            _puzzle.Input = _puzzle.Input.Replace("\n", " ").Replace("  ", "\n");

            var groups = _puzzle.ToEnumerable((line) => line.Split(" ").SelectMany(arry => arry).ToHashSet());

            var count = groups.Select(group => group.Count).Sum();

            Assert.That(count, Is.EqualTo(6587));
        }

        [Test]
        public void Part2()
        {
            var count = _puzzle.Input.Trim().Split("\n\n")
                .Select(x => x.Split("\n").Select(l => l.ToCharArray().Distinct()))
                .Select(g => g.Aggregate((prev, next) => prev.Intersect(next).ToArray()).Count())
                .Sum();

            Assert.That(count, Is.EqualTo(3235));
        }
    }
}