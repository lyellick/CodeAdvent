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
                            switch (line[2])
                            {
                                case "..":
                                    virdir.MoveUp();
                                    break;
                                default:
                                    virdir.MoveDown(line[2]);
                                    break;
                            }
                            index++;
                            break;
                        case "ls":
                            index++;

                            do
                            {
                                var entity = history[index];

                                bool isDir = entity.Contains("dir");

                                if (isDir)
                                {
                                    virdir.Current.AddDirectory(entity[1]);
                                }
                                else
                                {
                                    string name = entity[1];
                                    bool canParse = int.TryParse(entity[0], out int size);

                                    virdir.Current.AddFile(name, size);
                                }

                                index++;
                            } while (!history[index].Contains("$"));
                            break;
                    }
                }
            } while (index < history.Length);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}