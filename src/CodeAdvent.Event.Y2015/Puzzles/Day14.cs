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
            _input = await CodeAdventData.GetData(0, 0);

            Assert.That(_input, Is.Not.Null.Or.Empty);
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