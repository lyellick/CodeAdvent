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

            int rounds = 20, modifier = 3;

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

                        monkey.Holding[item] = Convert.ToInt64(Math.Floor((decimal)monkey.Holding[item] / modifier));

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

            var top = monkeys.OrderByDescending(monkey => monkey.Inspections).Take(2).ToArray();

            var count = top[0].Inspections * top[1].Inspections;

            Assert.That(count, Is.EqualTo(54054));
        }

        [Test]
        public void Part2()
        {
            string pattern = @"Monkey (.+):\n\s+Starting items: (.+)\n\s+Operation: new = old ([\*|+] .+)\n\s+Test: divisible by (\d+)\n\s+If true: throw to monkey (\d+)\n\s+If false: throw to monkey (\d+)";

            var monkeys = _puzzle.ToEnumerable<Monkey>("\n\n", pattern, (monkey) => new(monkey)).ToArray();

            int rounds = 10000, modifier = 1, limit = monkeys.Aggregate(1, (iteration, monkey) => iteration * monkey.Test.DivisibleBy);

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

                        monkey.Holding[item] = Convert.ToInt64(Math.Floor((decimal)monkey.Holding[item] / modifier));

                        if (monkey.Holding[item] >= limit)
                            monkey.Holding[item] %= limit;

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

            var top = monkeys.OrderByDescending(monkey => monkey.Inspections).Take(2).ToArray();

            var count = top[0].Inspections * top[1].Inspections;

            Assert.That(count, Is.EqualTo(14314925001));
        }
    }

    public class Monkey
    {
        public int Id { get; set; }

        public long Inspections { get; set; }

        public int Limit { get; set; }

        public List<long> Holding { get; set; }

        public string[] Operation { get; set; }

        public MonkeyTest Test { get; set; }

        public Monkey(Match match)
        {
            Id = int.Parse(match.Groups[1].Value);

            Inspections = 0;

            Holding = Array.ConvertAll(match.Groups[2].Value.Split(", "), i => long.Parse(i)).ToList();

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