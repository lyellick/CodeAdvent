using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 13: Knights of the Dinner Table
    /// </summary>
    public class Day13
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 13);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var plan = ProcessOptimalSeatingArrangement(_input).OrderByDescending(arrangement => arrangement.units.Sum());

            int sum = plan.First().units.Sum();

            Assert.That(sum, Is.EqualTo(664));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (IList<string> permutation, IList<string[]> splits, int[] units)[] ProcessOptimalSeatingArrangement(string input)
        {
            var requests = MapIdealNeighbors(input);

            var guests = requests.Select(request => request.guest).ToHashSet().ToList();

            var guestPermutations = guests.GeneratePermutations();

            var datasets = GenerateDatasets(guestPermutations).ToArray();

            for (int i = 0; i < datasets.Length; i++)
            {
                List<int> round = new();

                foreach (var split in datasets[i].splits)
                {
                    var left = requests.FirstOrDefault(request => split[0].Contains(request.guest) && split[1].Contains(request.neighbor));
                    var right = requests.FirstOrDefault(request => split[1].Contains(request.guest) && split[0].Contains(request.neighbor));

                    switch (left.outcome)
                    {
                        case Outcome.Gain: round.Add(left.units); break;
                        case Outcome.Lose: round.Add(left.units * -1); break;
                        case Outcome.None: default: break;
                    }

                    switch (right.outcome)
                    {
                        case Outcome.Gain: round.Add(right.units); break;
                        case Outcome.Lose: round.Add(right.units * -1); break;
                        case Outcome.None: default: break;
                    }
                }

                datasets[i].units = round.ToArray();
            }

            return datasets;
        }

        private IEnumerable<(IList<string> permutation, IList<string[]> splits, int[] units)> GenerateDatasets(IEnumerable<IList<string>> permutations)
        {
            foreach (var permutation in permutations)
            {
                List<string[]> splits = new();

                for (int i = 0; i < permutation.Count; i++)
                    if (i < permutation.Count - 1)
                        splits.Add(new string[] { permutation[i], permutation[i + 1] });

                // Catch the loop around so the last guest can be matched up with the first guest in the permuation.
                splits.Add(new string[] { permutation[^1], permutation[0] });

                yield return (permutation, splits, Array.Empty<int>());
            }
        }

        private IEnumerable<(string guest, Outcome outcome, int units, string neighbor)> MapIdealNeighbors(string input)
        {
            Regex pattern = new(@"(.*) would (.*) (.*) happiness units by sitting next to (.*).");

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                var found = pattern.Match(line);

                if (Enum.TryParse(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(found.Groups[2].Value), out Outcome outcome) && int.TryParse(found.Groups[3].Value, out int units))
                    yield return (found.Groups[1].Value, outcome, units, found.Groups[4].Value);
            }
        }

        private enum Outcome { Gain, Lose, None }
    }
}