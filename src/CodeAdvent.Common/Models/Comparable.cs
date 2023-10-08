using CodeAdvent.Common.Extensions;
using Newtonsoft.Json.Linq;

namespace CodeAdvent.Common.Models
{
    public class Comparable : IComparable<Comparable>
    {
        public JArray Item { get; }

        public Comparable(JArray item) => Item = item;

        public int CompareTo(Comparable to)
        {
            if (ReferenceEquals(Item, to.Item))
                return 0;

            var result = Item.CompareTo(to.Item);

            return result == true ? 1 : result == false ? -1 : 0;
        }
    }
}
