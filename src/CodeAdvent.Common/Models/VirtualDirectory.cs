using System.Text.Json.Serialization;

namespace CodeAdvent.Common.Models
{
    public class VirtualDirectory
    {
        public Node Current { get; set; }

        public Node Root { get; set; }

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

    public class Node
    {
        public string Name { get; set; }

        public List<Node> Children { get; set; }

        public List<Entity> Entities { get; set; }

        [JsonIgnore]
        public virtual Node Parent { get; set; }

        public Node(string name, Node parent)
        {
            Parent = parent;
            Name = name;
            Children = new();
            Entities = new();
        }

        public void AddNode(string name) => Children.Add(new(name, this));

        public void AddEntity(string name, int size) => Entities.Add(new(name, size));
    }

    public class Entity
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public Entity (string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
