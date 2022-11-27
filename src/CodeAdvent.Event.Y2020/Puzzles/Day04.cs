using CodeAdvent.Common.Extensions;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 4: Passport Processing
    /// </summary>
    public class Day04
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 4);

            _puzzle.Input = _puzzle.Input.Replace("\n", " ").Replace("  ", "\n");

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var fields = new Dictionary<string, string> { { "byr", "Birth Year" }, { "iyr", "Issue Year" }, { "eyr", "Expiration Year" }, { "hgt", "Height" }, { "hcl", "Hair Color" }, { "ecl", "Eye Color" }, { "pid", "Passport ID" }, { "cid", "Country ID" } };

            var passports = _puzzle.ToEnumerable(@"(\w+):([^\s]+)", (matches) => matches.ToDictionary(match => match.Groups[1].Value, match => match.Groups[2].Value)).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}