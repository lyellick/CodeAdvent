namespace CodeAdvent.Event.Y2016.Puzzles
{
    /// <summary>
    /// </summary>
    [Ignore("template unit test does not need to be ran")]
    public class Template
    {
        private CodeAdventEvent _event;

        [SetUp]
        public async Task Setup()
        {
            _event = await CodeAdventData.GetEvent(0, 0);

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