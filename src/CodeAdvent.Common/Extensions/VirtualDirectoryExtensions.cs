using CodeAdvent.Common.Models;

namespace CodeAdvent.Common.Extensions
{
    public static class VirtualDirectoryExtensions
    {
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

        public static int ListDirectory(this VirtualDirectory virdir, string[][] history, int index)
        {
            bool process = true;

            index++;

            do
            {
                var entity = history[index];

                bool isDir = entity.Contains("dir");

                if (isDir)
                {
                    virdir.Current.AddNode(entity[1]);
                }
                else
                {
                    string name = entity[1];
                    bool canParse = int.TryParse(entity[0], out int size);

                    virdir.Current.AddEntity(name, size);
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
    }
}
