namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// </summary>
    public class Template
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(0, 0);
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