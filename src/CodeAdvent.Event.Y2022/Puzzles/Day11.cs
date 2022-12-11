using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 11: Monkey in the Middle
    /// </summary>
    public class Day11
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 11);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            string pattern = @"Monkey (.+):\n\s+Starting items: (.+)\n\s+Operation: new = old ([\*|+] .+)\n\s+Test: divisible by (\d+)\n\s+If true: throw to monkey (\d+)\n\s+If false: throw to monkey (\d+)";

            var monkeys = _puzzle.ToEnumerable<Monkey>("\n\n", pattern, (monkey) => new(monkey)).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }

    public class Monkey
    {
        public int Id { get; set; }

        public int[] StartingItems { get; set; }

        public string Operation { get; set; }

        public MonkeyTest Test { get; set; }

        public Monkey(Match match)
        {
            Id = int.Parse(match.Groups[1].Value);

            StartingItems = Array.ConvertAll(match.Groups[2].Value.Split(", "), i => int.Parse(i));

            Operation = match.Groups[3].Value;

            Test = new MonkeyTest(match);
        }
    }

    public class MonkeyTest
    {
        public int DivisibleBy { get; set; }

        public int True { get; set; }

        public int False { get; set; }

        public MonkeyTest(Match match)
        {
            DivisibleBy = int.Parse(match.Groups[4].Value);

            True = int.Parse(match.Groups[5].Value);

            False = int.Parse(match.Groups[6].Value);
        }
    }
}