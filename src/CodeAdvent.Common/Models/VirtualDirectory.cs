namespace CodeAdvent.Common.Models
{
    public class VirtualDirectory
    {
        public Directory Current { get; set; }

        public Directory Root { get; set; }

        public VirtualDirectory(string root)
        {
            Root = new(root, null);
            Current = Root;
        }

        public void MoveUp()
        {
            Current = Current.Parent;
        }

        public void MoveDown(string name)
        {
            bool exists = Current.Children.Any(child => child.Name == name);

            if (exists)
                Current = Current.Children.First(child => child.Name == name);
        }
    }

    public class Directory
    {
        public string Name { get; set; }

        public List<Directory> Children { get; set; }

        public List<(string name, int size)> Files { get; set; }

        public virtual Directory Parent { get; set; }

        public Directory(string name, Directory parent)
        {
            Parent = parent;
            Name = name;
            Children = new();
            Files = new();
        }

        public void AddDirectory(string name) => Children.Add(new(name, this));

        public void AddFile(string name, int size) => Files.Add((name, size));
    }
}
