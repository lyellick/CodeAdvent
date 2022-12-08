using CodeAdvent.Common.Models;

namespace CodeAdvent.Common.Extensions
{
    public static class VirtualDirectoryExtensions
    {
        public static VirtualDirectory MapHistory(this VirtualDirectory vdir, string[][] history)
        {
            int index = 0;

            do
            {
                var line = history[index];

                if (line.Contains("$"))
                {
                    switch (line[1])
                    {
                        case "cd":
                            index = vdir.ChangeDirectory(index, line[2]);
                            break;
                        case "ls":
                            index = vdir.Current.ListDirectory(history, index);
                            break;
                    }
                }

            } while (index < history.Length);

            return vdir;
        }

        public static int ChangeDirectory(this VirtualDirectory virdir, int index, string name)
        {
            switch (name)
            {
                case "..":
                    virdir.MoveUp();
                    break;
                default:
                    virdir.MoveDown(name);
                    break;
            }
            index++;
            return index;
        }

        public static int ListDirectory(this Node node, string[][] history, int index)
        {
            bool process = true;

            index++;

            do
            {
                var entity = history[index];

                bool isDir = entity.Contains("dir");

                if (isDir)
                {
                    node.AddNode(entity[1]);
                }
                else
                {
                    string name = entity[1];
                    bool canParse = int.TryParse(entity[0], out int size);

                    node.AddEntity(name, size);
                }

                index++;

                if (index < history.Length)
                {
                    process = !history[index].Contains("$");
                }
                else
                {
                    process = false;
                }
            } while (process);

            return index;
        }

        public static int GetNodeSize(this Node node)
        {
            int size = node.Entities.Sum(entry => entry.Size);

            foreach (var child in node.Children)
                size += child.GetNodeSize();

            return size;
        }

        public static IEnumerable<(string directory, int size)> Crawl(this Node node)
        {
            List<(string directory, int size)> candidates = new();

            int size = node.GetNodeSize();

            candidates.Add((node.Name, size));

            foreach (var child in node.Children)
            {
                var found = child.Crawl();

                candidates.AddRange(found);
            }

            return candidates;
        }
    }
}
