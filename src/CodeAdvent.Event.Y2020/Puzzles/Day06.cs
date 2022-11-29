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

            //_puzzle.Input = "abc\n\na\nb\nc\n\nab\nac\n\na\na\na\na\n\nb";

            _puzzle.Input = _puzzle.Input.Replace("\n", " ").Replace("  ", "\n");

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var groups = _puzzle.ToEnumerable((line) => line.Split(" ").SelectMany(arry => arry).ToHashSet());

            var count = groups.Select(group => group.Count).Sum();

            Assert.That(count, Is.EqualTo(6587));
        }

        [Test]
        public void Part2()
        {
            var groups = _puzzle
                .ToEnumerable((line) => line.Split(" "))
                .Select(group => group.Select(section => section.ToHashSet().OrderBy(person => person).ToArray()).ToArray())
                .ToArray();

            List<int> answers = new();

            for (int group = 0; group < groups.Length; group++)
            {
                if (groups[group].Length == 1)
                {
                    answers.Add(1);
                }
                else
                {
                    Dictionary<char, int> counts = new();

                    for (int section = 0; section < groups[group].Length; section++)
                    {
                        for (int person = 0; person < groups[group][section].Length; person++)
                        {
                            char c = groups[group][section][person];

                            bool exists = counts.ContainsKey(c);

                            if (exists)
                            {
                                counts[c]++;
                            }
                            else
                            {
                                counts.Add(c, 1);
                            }
                        }
                    }

                    var answer = counts.Where(kvp => kvp.Value == groups[group].Length).Count();

                    answers.Add(answer);
                }
            }

            var count = answers.Sum();

            Assert.Pass();
        }
    }
}