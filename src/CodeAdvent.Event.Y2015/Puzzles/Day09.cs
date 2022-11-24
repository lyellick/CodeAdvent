using System.Data;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 9: All in a Single Night
    /// </summary>
    public class Day09
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

            var locationPermutations = locations.GeneratePermutations();

            var datasets = GenerateDataset(locationPermutations).ToArray();

            Parallel.For(0, datasets.Length, i =>
            {
                Parallel.ForEach(datasets[i].routes, route =>
                {
                    var found = distances.FirstOrDefault(distance => route.Contains(distance.start) && route.Contains(distance.end));

                    datasets[i].distance += found.distance;
                });
            });

            return datasets;
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
    }
}