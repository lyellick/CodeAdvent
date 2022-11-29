namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// </summary>
    [Ignore("template unit test does not need to be ran")]
    public class Template
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(0, 0);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
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