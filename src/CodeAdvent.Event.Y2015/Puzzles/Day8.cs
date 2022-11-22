namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 8: Matchsticks
    /// </summary>
    public class Day8
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 8);

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