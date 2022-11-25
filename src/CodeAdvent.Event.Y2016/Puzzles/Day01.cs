using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2016.Puzzles
{
    /// <summary>
    /// Day 1: No Time for a Taxicab
    /// </summary>
    public class Day01
    {
        private CodeAdventEvent _event;

        [SetUp]
        public async Task Setup()
        {
            _event = await CodeAdventData.GetEvent(2016, 1);

            Assert.That(_event.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            List<(Cardinal cardinal, int x, int y)> path = new() { (Cardinal.North, 0, 0) };

            var directions = _event.Map<(string direction, int forward)>(@"[a-zA-Z0-9]+", (match) => (match.Value[0].ToString(), int.Parse(match.Value[1].ToString()))).ToArray();

            foreach (var direction in directions)
            {
                var previous = path[^1];
            }

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private enum Cardinal { North, South, East, West }
    }
}