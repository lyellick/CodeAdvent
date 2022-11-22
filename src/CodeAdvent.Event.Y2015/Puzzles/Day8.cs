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
            var sizes = CalcListSize(_input);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (int stringLiteralSize, int inMemorySize) CalcListSize(string input)
        {
            int stringLiteralSize = 0;
            int inMemorySize = 0;

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {

            }

            return (stringLiteralSize, inMemorySize);
        }
    }
}