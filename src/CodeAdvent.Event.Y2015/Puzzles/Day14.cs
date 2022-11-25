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
            _input = await CodeAdventData.GetInput(2015, 14);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var results = ProcessReindeerSimulation(_input, 2503).OrderByDescending(simulation => simulation.distance);

            double distance = results.First().distance;

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (string reindeer, double speed, double duration, double rest, double distance)[] ProcessReindeerSimulation(string input, int seconds)
        {
            var reindeer = CodeAdventData.MapInput<(string reindeer, double speed, double duration, double rest, double distance)>(input, @"(.*) can fly (.*) km\/s for (.*) seconds, but then must rest for (.*) seconds.", (match) =>
            {
                if (double.TryParse(match.Groups[2].Value, out double speed) && double.TryParse(match.Groups[3].Value, out double duration) && double.TryParse(match.Groups[4].Value, out double rest))
                    return (match.Groups[1].Value, speed, duration, rest, 0);

                return ("", 0, 0, 0, 0);
            }).ToArray();

            for (int i = 0; i < reindeer.Length; i++)
            {
                reindeer[i].distance = Math.Ceiling(seconds / (reindeer[i].duration + reindeer[i].rest)) * (reindeer[i].speed * reindeer[i].duration);
            }

            return reindeer;
        }
    }
}