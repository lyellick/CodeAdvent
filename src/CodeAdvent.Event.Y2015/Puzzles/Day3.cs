using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 3: Perfectly Spherical Houses in a Vacuum
    /// </summary>
    public class Day3
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 3);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int visits = Visits(_input).Count();

            Assert.That(visits, Is.EqualTo(2081));
        }

        [Test]
        public void Part2()
        {
            var santa = Visits(string.Join("", _input.Where((c, i) => i % 2 == 0)));

            var robot = Visits(string.Join("", _input.Where((c, i) => i % 2 != 0)));

            var visited = santa.Concat(robot).ToHashSet().Count;

            Assert.That(visited, Is.EqualTo(2341));
        }

        private HashSet<(int x, int y)> Visits(string input)
        {
            HashSet<(int x, int y)> seen = new() { (0, 0) };

            int x = 0, y = 0;

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '^': y++; break;
                    case 'v': y--; break;
                    case '>': x++; break;
                    case '<': x--; break;
                }

                seen.Add((x, y));
            }

            return seen;
        }
    }
}