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
            Assert.Pass();
        }
    }
}