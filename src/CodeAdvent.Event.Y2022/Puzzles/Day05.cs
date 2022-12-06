using CodeAdvent.Common.Extensions;
using System.Linq;
using System.Security.Cryptography;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 5: Supply Stacks    
    /// </summary>
    public class Day05
    {  
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 5);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var stacks = _puzzle.ToEnumerable(
                @"(.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3})", 
                (match) => match.Groups.Values.Select(val => val.Value).Skip(1).ToArray()).Take(8).ToArray();

            var instructions = _puzzle.ToEnumerable<(int stack, int from, int to)>(
                @"move (.*) from (.*) to (.*)",
                (match, isMatch) => isMatch ?  (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[2].Value)) : (0,0,0)).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}