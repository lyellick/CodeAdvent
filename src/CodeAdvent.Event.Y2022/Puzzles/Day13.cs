using System.Net.Sockets;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 13: Distress Signal
    /// </summary>
    public class Day13
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 13);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }   

        [Test]
        public void Part1()
        {   
            var pairs = _puzzle
                .ToEnumerable<(JArray left, JArray right)>("\n\n", "\n", (pair) => (JArray.Parse(pair[0]), JArray.Parse(pair[1])))
                .ToArray();

            var range = Enumerable.Range(1, pairs.Length);

            var valid = pairs.Select(pair => pair.left.CompareTo(pair.right)).ToArray();

            var result = valid.Zip(range, (prev, next) => prev == true ? next : 0);

            Assert.That(result.Sum(), Is.EqualTo(5623));
        }

        [Test]
        public void Part2()
        {
            var packets = _puzzle
                .ToEnumerable("\n\n", "\n", (pair) => new JArray[] { JArray.Parse(pair[0]), JArray.Parse(pair[1]) })
                .SelectMany(pair => pair)
                .ToList();

            packets.AddRange(new[] { "[[2]]", "[[6]]" }.Select(packet => JArray.Parse(packet)));

            var end = packets.Skip(packets.Count - 2).ToArray();

            var stream = packets.OrderByDescending(packet => new Comparable(packet)).ToArray();

            var key = (Array.IndexOf(stream, end[0]) + 1) * (Array.IndexOf(stream, end[1]) + 1);

            Assert.That(key, Is.EqualTo(20570));
        }
    }
}