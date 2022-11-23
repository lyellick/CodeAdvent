namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 2: I Was Told There Would Be No Math
    /// </summary>
    public class Day02
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 2);

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int paper = Boxes(_input).Sum(d => d.paper);

            Assert.That(paper, Is.EqualTo(1586300));
        }

        [Test]
        public void Part2()
        {
            int ribbon = Boxes(_input).Sum(d => d.ribbon);

            Assert.That(ribbon, Is.EqualTo(3737498));
        }

        private IEnumerable<(int box, int paper, int ribbon)> Boxes(string input)
        {
            int box = 1;

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine(), box++)
            {
                int[] dimensions = line.Split('x').Select(d => int.Parse(d)).ToArray();

                int paper = CalcPaper(dimensions[0], dimensions[1], dimensions[2]);

                int ribbon = CalcRibbon(dimensions[0], dimensions[1], dimensions[2]);

                yield return (box, paper, ribbon);
            }
        }

        private int CalcPaper(int l, int w, int h)
        {
            int[] x = new int[] { l * w, w * h, h * l };
            
            return x.Sum(y => y * 2) + x.Min();
        }

        private int CalcRibbon(int l, int w, int h)
        {
            int[] values = new int[] { l, w, h };

            Array.Sort(values);

            int ribbon = values[0] + values[0] + values[1] + values[1];
            
            int bow = l * w * h;

            return ribbon + bow;
        }
    }
}