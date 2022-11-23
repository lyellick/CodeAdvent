using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 9: All in a Single Night
    /// </summary>
    public class Day9
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 9);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            ProcessRoutes(_input);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private void ProcessRoutes(string input)
        {
            var routes = MapRoutes(input);

            var locations = GetDistinctRoutes(routes);

            var permutations = GeneratePermutations(locations);
        }

        private IEnumerable<(string start, string end, int distance)> MapRoutes(string input)
        {
            Regex parts = new(@"(.*)\sto\s(.*)\s=\s(.*)");

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                var route = parts.Match(line);

                yield return (route.Groups[1].Value, route.Groups[2].Value, int.Parse(route.Groups[3].Value));
            }
        }

        private string[] GetDistinctRoutes(IEnumerable<(string start, string end, int distance)> routes) => routes
                .Select(route => new string[] { route.start, route.end })
                .SelectMany(locations => locations)
                .ToHashSet()
                .ToArray();

        private IEnumerable<IList<T>> GeneratePermutations<T>(IList<T> source)
        {
            int length = source.Count;

            var startIndices = new int[length];
            var variationElements = new HashSet<T>();

            while (startIndices[0] < source.Count)
            {
                var variation = new List<T>(length);
                var valid = true;
                for (int i = 0; i < length; ++i)
                {
                    var element = source[startIndices[i]];
                    if (variationElements.Contains(element))
                    {
                        valid = false;
                        break;
                    }
                    variation.Add(element);
                    variationElements.Add(element);
                }
                if (valid)
                    yield return variation;

                startIndices[length - 1]++;
                for (int i = length - 1; i > 0; --i)
                {
                    if (startIndices[i] >= source.Count)
                    {
                        startIndices[i] = 0;
                        startIndices[i - 1]++;
                    }
                    else
                        break;
                }
                variationElements.Clear();
            }
        }
    }
}