namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 7: Handy Haversacks
    /// </summary>
    public class Day07
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 7);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var nodes = _puzzle.ToEnumerable<(string node, (int count, string node)[] children)>(@"(.*) bags contain (.*)", (match) => 
                {
                    string parent = match.Groups[1].Value;

                    string[] bags = match.Groups[2].Value.Contains("no other")
                        ? Array.Empty<string>()
                        : match.Groups[2].Value.Split(",").Select(bag => bag.Replace(".", "").Replace("bags", "").Replace("bag", "").Trim()).ToArray();

                    var children = bags.Select(bag => (int.Parse(bag.Split(' ', 2)[0]), bag.Split(' ', 2)[1])).ToArray();

                    return (parent, children);
                }).ToArray();

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}