using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var pairs = _puzzle.ToEnumerable<(JArray left, JArray right)>("\n\n", "\n", (pair) => (JArray.Parse(pair[0]), JArray.Parse(pair[1]))).ToArray();

            foreach (var pair in pairs)
            {
                
            }

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}