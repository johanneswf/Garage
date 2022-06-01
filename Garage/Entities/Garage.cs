namespace Garage.Entities
{
    internal class Garage<T> : IEnumerable<T>
    {
        private readonly T[] _garage;
        private readonly int _size;

        public Garage(int size)
        {
            _size = size;
            _garage = new T[size];
        }

        public bool Add(T item)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_garage[i] is null)
                {
                    _garage[i] = item;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _size; i++)
            {
                if (_garage[i] is not null)
                {
                    yield return _garage[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
