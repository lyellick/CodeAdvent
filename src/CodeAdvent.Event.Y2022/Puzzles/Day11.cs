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
            int rounds = 20;

            _puzzle.Input = "Monkey 0:\n  Starting items: 79, 98\n  Operation: new = old * 19\n  Test: divisible by 23\n    If true: throw to monkey 2\n    If false: throw to monkey 3\n\nMonkey 1:\n  Starting items: 54, 65, 75, 74\n  Operation: new = old + 6\n  Test: divisible by 19\n    If true: throw to monkey 2\n    If false: throw to monkey 0\n\nMonkey 2:\n  Starting items: 79, 60, 97\n  Operation: new = old * old\n  Test: divisible by 13\n    If true: throw to monkey 1\n    If false: throw to monkey 3\n\nMonkey 3:\n  Starting items: 74\n  Operation: new = old + 3\n  Test: divisible by 17\n    If true: throw to monkey 0\n    If false: throw to monkey 1";

            string pattern = @"Monkey (.+):\n\s+Starting items: (.+)\n\s+Operation: new = old ([\*|+] .+)\n\s+Test: divisible by (\d+)\n\s+If true: throw to monkey (\d+)\n\s+If false: throw to monkey (\d+)";

            var monkeys = _puzzle.ToEnumerable<Monkey>("\n\n", pattern, (monkey) => new(monkey)).ToArray();

            do
            {
                for (int id = 0; id < monkeys.Length; id++)
                {
                    Monkey monkey = monkeys[id];

                    monkey.Inspections += monkey.Holding.Count;

                    for (int item = 0; item < monkey.Holding.Count; item++)
                    {
                        switch (monkey.Operation[1])
                        {
                            case "old":
                                monkey.Holding[item] *= monkey.Holding[item];
                                break;
                            default:
                                if (monkey.Operation[0] == "+")
                                    monkey.Holding[item] += int.Parse(monkey.Operation[1]);
                                else
                                    monkey.Holding[item] *= int.Parse(monkey.Operation[1]);
                                break;
                        }

                        // Missing divided by 3?
                        if (monkey.Holding[item] % monkey.Test.DivisibleBy == 0)
                        {
                            monkeys[monkey.Test.True].Holding.Add(monkey.Holding[item]);
                        }
                        else
                        {
                            monkeys[monkey.Test.False].Holding.Add(monkey.Holding[item]);
                        }
                    }

                    monkey.Holding.Clear();
                }

                rounds--;
            } while (rounds != 0);

            var inspections = monkeys.Select(monkey => (monkey.Id, monkey.Inspections)).ToArray();

            var top = monkeys.OrderByDescending(monkey => monkey.Inspections).Take(2).ToArray();

            var count = top[0].Inspections * top[1].Inspections;

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

        public int Inspections { get; set; }

        public List<int> Holding { get; set; }

        public string[] Operation { get; set; }

        public MonkeyTest Test { get; set; }

        public Monkey(Match match)
        {
            Id = int.Parse(match.Groups[1].Value);

            Inspections = 0;

            Holding = Array.ConvertAll(match.Groups[2].Value.Split(", "), i => int.Parse(i)).ToList();

            Operation = match.Groups[3].Value.Split(" ");

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