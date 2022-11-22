using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 8: Matchsticks
    /// </summary>
    public class Day8
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 8);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            _input = @"""""
""abc""
""aaa\""aaa""
""\x27""";

            var sizes = CalcListSize(_input);

            int size = sizes.stringLiteralSize - sizes.inMemorySize;

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (int stringLiteralSize, int inMemorySize) CalcListSize(string input)
        {
            List<int> stringLiteralSize = new();
            List<int> inMemorySize = new();

            Regex hex = new(@"\\x\w{2}");
            Regex backslash = new(@"\\\\");
            Regex doublequotes = new(@"\\""");

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                int stringSize = line.Length;

                var foundHex = hex.Matches(line);
                var foundBackslashes = backslash.Matches(line);
                var foundDoublequotes = doublequotes.Matches(line);

                line = hex.Replace(line, "");
                line = backslash.Replace(line, "");
                line = doublequotes.Replace(line, "");

                line = line.Remove(0, 1);
                line = line.Remove(line.Length - 1, 1);

                int memorySize = line.Length + foundHex.Count + foundBackslashes.Count + foundDoublequotes.Count;

                stringLiteralSize.Add(stringSize);
                inMemorySize.Add(memorySize);
            }

            return (stringLiteralSize.Sum(), inMemorySize.Sum());
        }
    }
}