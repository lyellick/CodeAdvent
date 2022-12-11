namespace CodeAdvent.Common.Models
{
    public class NavigationList<T> : List<T>
    {
        private int _currentIndex = 0;

        public int CurrentIndex
        {
            get
            {
                if (_currentIndex > Count - 1) { _currentIndex = Count - 1; }
                if (_currentIndex < 0) { _currentIndex = 0; }
                return _currentIndex;
            }
            set { _currentIndex = value; }
        }

        public void MoveNext() => _currentIndex++;

        public void MovePrevious() => _currentIndex--;

        public T Current
        {
            get { return this[CurrentIndex]; }
        }
    }
}
