using System.Diagnostics;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 14: Reindeer Olympics
    /// </summary>
    public class Day14
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 14);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var results = ProcessReindeerSimulation(_input, 2503).OrderByDescending(simulation => simulation.distance);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (string reindeer, int speed, int duration, int rest, int distance, bool fly)[] ProcessReindeerSimulation(string input, int seconds)
        {
            var reindeer = MapReindeerStats(input).ToArray();

            reindeer = new (string reindeer, int speed, int duration, int rest, int distance, bool fly)[] { ("Comet", 14, 10, 127, 0, true), ("Dancer", 16, 11, 162, 0, true) };

            seconds = 1000;

            int second = 0;

            do
            {
                second++;

                for (int i = 0; i < reindeer.Length; i++)
                {
                    if (reindeer[i].fly)
                    {
                        reindeer[i].distance += reindeer[i].speed;

                        reindeer[i].fly = second % reindeer[i].duration != 0;
                    }
                    else
                    {
                        reindeer[i].fly = second % reindeer[i].rest == 0;
                    }
                }
            } while (second != seconds);

            return reindeer;
        }

        private IEnumerable<(string reindeer, int speed, int duration, int rest, int distance, bool fly)> MapReindeerStats(string input)
        {
            Regex pattern = new(@"(.*) can fly (.*) km\/s for (.*) seconds, but then must rest for (.*) seconds.");

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                var found = pattern.Match(line);

                if (int.TryParse(found.Groups[2].Value, out int speed) && int.TryParse(found.Groups[3].Value, out int duration) && int.TryParse(found.Groups[4].Value, out int rest))
                    yield return (found.Groups[1].Value, speed, duration, rest, 0, true);
            }
        }
    }
}