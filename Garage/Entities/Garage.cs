﻿namespace Garage.Entities
{
    internal class Garage<T> : IEnumerable<T>
    {
        private readonly T[] _garage;
        public readonly int Size;

        public Garage(int size)
        {
            Size = size;
            _garage = new T[size];
        }

        public void Add(T item)
        {
            // We iterate over our garage array
            for (int i = 0; i < Size; i++)
            {
                // We add our item to the first null element in our array
                if (_garage[i] is null)
                {
                    _garage[i] = item;
                    break;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
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
