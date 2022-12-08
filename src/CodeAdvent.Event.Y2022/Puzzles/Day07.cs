using Newtonsoft.Json;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 7: No Space Left On Device
    /// </summary>
    public class Day07
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 7);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            VirtualDirectory virdir = new("/");

            var history = _puzzle.ToEnumerable((line) => line.Split(" ")).Skip(1).ToArray();
            
            int index = 0;

            do
            {
                var line = history[index];

                if (line.Contains("$"))
                {
                    switch (line[1])
                    {
                        case "cd":
                            index = virdir.ChangeDirectory(index, line[2]);
                            break;
                        case "ls":
                            index = virdir.ListDirectory(history, index);
                            break;
                    }
                }
            } while (index < history.Length);

            string json = JsonConvert.SerializeObject(virdir.Root, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}