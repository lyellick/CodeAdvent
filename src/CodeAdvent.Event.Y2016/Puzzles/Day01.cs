namespace CodeAdvent.Event.Y2016.Puzzles
{
    /// <summary>
    /// Day 1: No Time for a Taxicab
    /// </summary>
    public class Day01
    {
        private CodeAdventEvent _event;

        [SetUp]
        public async Task Setup()
        {
            _event = await CodeAdventData.GetEvent(2016, 1);

            Assert.That(_event.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}