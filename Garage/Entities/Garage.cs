namespace Garage.Entities
{
    internal class Garage<T> : IEnumerable<T>
        where T : IVehicle
    {
        private T[] garage;

        public readonly int Size;
        public int Count;

        public Garage(int size)
        {
            Size = size;
            garage = new T[size];
        }

        public void Add(T item)
        {
            for (int i = 0; i < Size; i++)
            {
                if (garage[i] is null)
                {
                    garage[i] = item;
                    Count++;
                    break;
                }
            }
        }

        public bool Remove(T found)
        {
            for (int i = 0; i < garage.Length; i++)
            {
                if (garage.Equals(found))
                {
                    garage[i] = default!;
                    Count--;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
            {
                if (garage[i] is not null)
                {
                    yield return garage[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
