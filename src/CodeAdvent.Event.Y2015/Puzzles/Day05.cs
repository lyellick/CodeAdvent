using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 5: Doesn't He Have Intern-Elves For This?
    /// </summary>
    public class Day05   
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 5);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int count = ProcessPart1(_input).Count(line => line.isNice);

            Assert.That(count, Is.EqualTo(238));
        }

        [Test]
        public void Part2()
        {
            int count = ProcessPart2(_input).Count(line => line.isNice);

            Assert.That(count, Is.EqualTo(69));
        }

        private IEnumerable<(string line, bool isNice)> ProcessPart1(string input)
        {
            string[] illegalStrings = new string[] { "ab", "cd", "pq", "xy" };

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                Regex vowels = new(@"[aeiou]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex duplicates = new(@"(\w)\1{1}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                bool hasVowels = vowels.Matches(line).Count >= 3;
                bool hasDuplicates = duplicates.Matches(line).Count > 0;
                bool hasIllegalString = illegalStrings.Any(str => line.Contains(str));

                yield return (line, hasVowels && hasDuplicates && !hasIllegalString);
            }
        }

        private IEnumerable<(string line, bool isNice)> ProcessPart2(string input)
        {
            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                bool hasDuplicates = Enumerable.Range(0, line.Length - 1)
                                               .Any(i => line.IndexOf(line.Substring(i, 2), i + 2) >= 0);

                bool hasRepeats = Enumerable.Range(0, line.Length - 2)
                                            .Any(i => line[i].Equals(line[i + 2]));

                yield return (line, hasDuplicates && hasRepeats);
            }
        }
    }
}