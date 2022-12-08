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
            if (!Current.IsRoot)
                // TODO: Impliment recursive lookup of parent based off the current directory.
                Current = Find(Root, Current.Parent);
        }

        public Directory Find(Directory directory, Guid parrent)
        {
            Directory found = null;

            do
            {
                if (directory.Parent == parrent)
                    found = directory;

                foreach (var child in directory.Children)
                {
                    if (child.Parent == parrent)
                        found = child;

                    if (child.Children.Count == 0)
                        break;
                }
            } while (directory == null);

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

        public void AddDirectory(string name) => Children.Add(new(Parent, name));

        public void AddFile(string name, int size) => Files.Add((name, size));
    }
}
