namespace CodeAdvent.Event.Y2017.Puzzles
{
    /// <summary>
    /// Day 1: Inverse Captcha
    /// </summary>
    public class Day01
    {
        private CodeAdventEvent _event;

        [SetUp]
        public async Task Setup()
        {
            _event = await CodeAdventData.GetEvent(2017, 1);

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