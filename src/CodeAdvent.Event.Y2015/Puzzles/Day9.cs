using System.Data;
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
            var shortest = ProcessRoutes(_input).OrderBy(route => route.distance).First();

            Assert.That(shortest.distance, Is.EqualTo(251));
        }

        [Test]
        public void Part2()
        {
            var longest = ProcessRoutes(_input).OrderByDescending(route => route.distance).First();

            Assert.That(longest.distance, Is.EqualTo(898));
        }

        private (IList<string> permutation, IList<string[]> routes, int distance)[] ProcessRoutes(string input)
        {
            var distances = MapDistances(input);

            var locations = GetDistinctLocations(distances);

            var locationPermutations = GenerateLocationPermutations(locations);

            var datsets = GenerateDataset(locationPermutations).ToArray();

            for (int i = 0; i < datsets.Length; i++)
            {
                Parallel.ForEach(datsets[i].routes, route => 
                {
                    var found = distances.FirstOrDefault(distance => route.Contains(distance.start) && route.Contains(distance.end));

                    datsets[i].distance += found.distance;
                });
            }

            return datsets;
        }

        private IEnumerable<(IList<string> permutation, IList<string[]> routes, int distance)> GenerateDataset(IEnumerable<IList<string>> permutations)
        {
            foreach (var permutation in permutations)
            {
                List<string[]> routes = new();

                for (int i = 0; i < permutation.Count; i++)
                    if (i < permutation.Count - 1)
                        routes.Add(new string[] { permutation[i], permutation[i + 1] });

                yield return (permutation, routes, 0);
            }
        }

        private IEnumerable<(string start, string end, int distance)> MapDistances(string input)
        {
            Regex parts = new(@"(.*)\sto\s(.*)\s=\s(.*)");

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                var route = parts.Match(line);

                yield return (route.Groups[1].Value, route.Groups[2].Value, int.Parse(route.Groups[3].Value));
            }
        }

        private string[] GetDistinctLocations(IEnumerable<(string start, string end, int distance)> routes) => routes
                .Select(route => new string[] { route.start, route.end })
                .SelectMany(locations => locations)
                .ToHashSet()
                .ToArray();

        private IEnumerable<IList<T>> GenerateLocationPermutations<T>(IList<T> source)
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