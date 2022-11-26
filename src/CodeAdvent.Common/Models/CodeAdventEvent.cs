namespace CodeAdvent.Common.Models
{
    public class CodeAdventEvent
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public string Input { get; set; }

        public Uri Calendar { get; set;  }

        public Uri Puzzle { get; set; }
    }
}
