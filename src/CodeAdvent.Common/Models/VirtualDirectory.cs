namespace CodeAdvent.Common.Models
{
    public class VirtualDirectory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<VirtualDirectory> Directories { get; set; }

        public List<(Guid parent, string name, int size)> Files { get; set; }

        public VirtualDirectory(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Directories = new();
            Files = new();
        }

        public void AddDirectory(string name) => Directories.Add(new(name));

        public void AddFile(string name, int size) => Files.Add(new(Id, name, size));
    }
}
