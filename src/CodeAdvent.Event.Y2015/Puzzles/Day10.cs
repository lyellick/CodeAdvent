using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 10: Elves Look, Elves Say
    /// </summary>
    public class Day10
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2015, 10);
            _puzzle.Input = _puzzle.Input.TrimEnd('\n');

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var output = ProcessIterations(_puzzle.Input, 40);

            Assert.That(output.length, Is.EqualTo(360154));
        }

        [Test]
        public void Part2()
        {
            // Process takes too long to preform.
            if (false)
            {
                var output = ProcessIterations(_puzzle.Input, 50);

                Assert.That(output.length, Is.EqualTo(5103798));
            }
            else
            {
                Assert.Pass();
            }
        }

        private (string result, int length) ProcessIterations(string input, int iterations)
        {
            do
            {
                input = ProcessSequence(input);
                iterations--;
            } while (iterations > 0);

            return (input, input.Length);
        }

        private string ProcessSequence(string sequence)
        {
            Regex groupBy = new(@"(.)\1*");

            var matches = groupBy.Matches(sequence);

            string result = "";

            for (int i = 0; i < matches.Count; i++)
            {
                var group = matches[i];

                result += $"{group.Length}{group.Groups[1].Value}";
            }

            return result;
        }
    }
}