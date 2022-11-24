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
            _input = @"Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.";

            var plan = ProcessOptimalSeatingArrangement(_input).OrderByDescending(arrangement => arrangement.units);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (IList<string> permutation, IList<string[]> splits, int units)[] ProcessOptimalSeatingArrangement(string input)
        {
            var requests = MapIdealNeighbors(input);

            var guests = requests.Select(request => request.guest).ToHashSet().ToList();

            var guestPermutations = guests.GeneratePermutations();

            var datasets = GenerateDatasets(guestPermutations).ToArray();

            Parallel.For(0, datasets.Length, i => 
            {
                Parallel.ForEach(datasets[i].splits, split => 
                {
                    var found = requests.FirstOrDefault(request => split[0].Contains(request.guest) && split[1].Contains(request.neighbor));

                    switch (found.outcome)
                    {
                        case Outcome.Gain: datasets[i].units += found.units; break;
                        case Outcome.Lose: datasets[i].units -= found.units; break;
                        case Outcome.None: default: break;
                    }
                });
            });

            return datasets;
        }

        private IEnumerable<(IList<string> permutation, IList<string[]> splits, int units)> GenerateDatasets(IEnumerable<IList<string>> permutations)
        {
            foreach (var permutation in permutations)
            {
                List<string[]> splits = new();

                for (int i = 0; i < permutation.Count; i++)
                    if (i < permutation.Count - 1)
                        splits.Add(new string[] { permutation[i], permutation[i + 1] });

                // Catch the loop around so the last guest can be matched up with the first guest in the permuation.
                splits.Add(new string[] { permutation[^1], permutation[0] });

                yield return (permutation, splits, 0);
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