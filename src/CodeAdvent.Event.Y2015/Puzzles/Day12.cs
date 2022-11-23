using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 12: JSAbacusFramework.io
    /// </summary>
    public class Day12
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 12);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            Regex integers = new(@"-?\d+");

            var matches = integers.Matches(_input);

            int sum = matches.Sum(match => int.Parse(match.Value));

            Assert.That(sum, Is.EqualTo(191164));
        }

        [Test]
        public void Part2()
        {
            Regex integers = new(@"-?\d+");
            Regex curlyBraces = new(@"\{([^}]+)\}");



            Assert.Pass();
        }
    }
}