namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 1: Not Quite Lisp
    /// </summary>
    public class Day1
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 1);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int floor = Levels(_input).Last().floor;

            Assert.That(floor, Is.EqualTo(138));
        }

        [Test]
        public void Part2()
        {
            int position = Levels(_input).First(c => c.floor.Equals(-1)).position;

            Assert.That(position, Is.EqualTo(1771));
        }

        private IEnumerable<(int position, int floor)> Levels(string input)
        {
            int floor = 0;

            for (int i = 0; i < input.Length; i++)
            {
                floor += _input[i].Equals('(') ? 1 : -1;
                
                yield return (i+1, floor);
            }
        }
    }
}