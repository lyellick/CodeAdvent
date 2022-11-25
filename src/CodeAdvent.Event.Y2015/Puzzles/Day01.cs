namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 1: Not Quite Lisp
    /// </summary>
    public class Day01
    {
        private CodeAdventEvent _event;

        [SetUp]
        public async Task Setup()
        {
            _event = await CodeAdventData.GetEvent(2015, 1);

            Assert.That(_event.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int floor = Levels(_event.Input).Last().floor;

            Assert.That(floor, Is.EqualTo(138));
        }

        [Test]
        public void Part2()
        {
            int position = Levels(_event.Input).First(c => c.floor.Equals(-1)).position;

            Assert.That(position, Is.EqualTo(1771));
        }

        private IEnumerable<(int position, int floor)> Levels(string input)
        {
            int floor = 0;

            for (int i = 0; i < input.Length; i++)
            {
                floor += _event.Input[i].Equals('(') ? 1 : -1;
                
                yield return (i+1, floor);
            }
        }
    }
}