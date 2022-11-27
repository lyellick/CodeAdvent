using CodeAdvent.Common.Extensions;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 2: Password Philosophy
    /// </summary>
    public class Day02
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 2);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var corruption = _puzzle.ToEnumerable<(int lowest, int highest, string character, string password)>(@"(.*)-(.*) (.*): (.*)", (match) => 
                (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value, match.Groups[4].Value)).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}