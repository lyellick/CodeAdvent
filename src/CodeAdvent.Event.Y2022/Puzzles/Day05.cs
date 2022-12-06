using System.Linq;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 5: Supply Stacks    
    /// </summary>
    public class Day05
    {  
        private CodeAdventPuzzle _puzzle;

        private List<List<string>> _stacks = new();

        private (int take, int from, int to)[] _instructions;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 5);

            var raw = _puzzle.ToEnumerable(
                @"(.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3})",
                (match) => match.Groups.Values.Select(val => val.Value).Skip(1).ToList())
                .Take(8).ToList();

            _stacks = raw.Pivot();

            _instructions = _puzzle.ToEnumerable<(int stack, int from, int to)>(
                @"move (.*) from (.*) to (.*)",
                (match, isMatch) => isMatch
                    ? (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value))
                    : (0, 0, 0))
                .Skip(10).ToArray();

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            foreach (var (take, from, to) in _instructions)
            {
                var collection = _stacks[from - 1].Take(take).ToArray();

                _stacks[from - 1] = _stacks[from - 1].Skip(take).ToList();

                foreach (var item in collection)
                    _stacks[to - 1] = _stacks[to - 1].Prepend(item).ToList();
            }

            var top = string.Join("", _stacks.Select(col => col.First().Replace("[", "").Replace("]", "")));

            Assert.That(top, Is.EqualTo("MQTPGLLDN"));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}