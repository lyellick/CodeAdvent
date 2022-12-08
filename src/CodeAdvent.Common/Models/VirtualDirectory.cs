namespace CodeAdvent.Common.Models
{
    public class VirtualDirectory
    {
        public Directory Current { get; set; }

        public Directory Root { get; set; }

        public VirtualDirectory(string root)
        {
            Root = new(Guid.NewGuid(), root, true);
            Current = Root;
        }

        public void MoveUp()
        {
            Current = Find(Root, Current.Parent);
        }

        public void MoveDown(string name)
        {
            bool exists = Current.Children.Any(child => child.Name == name);

            if (exists)
                Current = Current.Children.First(child => child.Name == name);
        }

        public Directory Find(Directory directory, Guid parent)
        {
            Directory found = null;

            do
            {
                if (directory.Id == parent)
                {
                    found = directory;
                }
                else
                {
                    if (directory.Children.Count > 0)
                    {
                        foreach (var child in directory.Children)
                        {
                            if (found != null)
                                break;

                            if (child.Id == parent)
                            {
                                found = child;
                            }
                            else
                            {
                                found = Find(child, parent);
                            }
                        }
                    }
                }
            } while (found == null);

            return found;
        }
    }

    public class Directory
    {
        public Guid Id { get; set; }

        public Guid Parent { get; set; }

        public string Name { get; set; }

        public List<Directory> Children { get; set; }

        public List<(string name, int size)> Files { get; set; }

        public bool IsRoot { get; set; }

        public Directory(Guid parent, string name, bool isRoot = false)
        {
            Id = isRoot ? parent : Guid.NewGuid();
            Parent = parent;
            Name = name;
            Children = new();
            Files = new();
            IsRoot = isRoot;
        }

        public void AddDirectory(string name) => Children.Add(new(Id, name));

        public void AddDirectories(string[] names) => Array.ForEach(names, name => Children.Add(new(Id, name)));

        public void AddFile(string name, int size) => Files.Add((name, size));

        public void AddFiles((string name, int size)[] files) => Files.AddRange(files);
    }
}
