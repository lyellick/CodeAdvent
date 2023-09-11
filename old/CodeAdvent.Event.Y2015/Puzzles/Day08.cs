using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 8: Matchsticks
    /// </summary>
    public class Day08
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2015, 8);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var sizes = CalcListSize(_puzzle.Input);

            int size = sizes.stringLiteralSize - sizes.inMemorySize;

            Assert.That(size, Is.EqualTo(1333));
        }

        [Test]
        public void Part2()
        {
            var sizes = CalcListSize(_puzzle.Input, true);

            int size = sizes.inMemorySize - sizes.stringLiteralSize;

            Assert.That(size, Is.EqualTo(2046));
        }

        private (int stringLiteralSize, int inMemorySize) CalcListSize(string input, bool isPart2 = false)
        {
            List<int> stringLiteralSize = new();
            List<int> inMemorySize = new();

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                int stringSize = line.Length;

                int memorySize = !isPart2 
                    ? Regex.Unescape(line[1..^1]).Length 
                    : $"\"{line.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"".Length;

                stringLiteralSize.Add(stringSize);
                inMemorySize.Add(memorySize);
            }

            return (stringLiteralSize.Sum(), inMemorySize.Sum());
        }
    }
}