using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomList.Models
{
    public class MyCustomList<T> : IEnumerable<T>
    {
        private int _capacity = 1;
        private T[] _listContent;
        private int _count = 0;

        public int Capacity { get { return _capacity; } set { if (value >= 0) _capacity = value; } }
        public int Count { get { return _count; } }

        public T this[int index]
        {
            get { return _listContent[index]; }
            set { _listContent[index] = value; }
        }

        public MyCustomList(int capacity = 1)
        {
            _count = 0;
            Capacity = capacity;
            _listContent = new T[Capacity];
        }

        public void Clear()
        {
            _listContent = new T[Capacity];
        }

        public void Add(T element)
        {
            if(Count >= Capacity)
            {
                Array.Resize(ref _listContent, _listContent.Length + 1);
                _listContent[^1] = element;
                _capacity++;
            }
            else
            {
                _listContent[_count] = element;
            }
            _count++;
        }

        public void AddRange(IEnumerable<T> iEnumerable)
        {
            foreach (T item in iEnumerable)
            {
                Add(item);
            }
        }

        public void Remove(T element)
        {
            for (int i = 0; i < _listContent.Length; i++)
            {
                if (_listContent[i].Equals(element))
                {
                    for (int j = _listContent.Length - 1; j >= 0; j--)
                    {
                        if(_listContent[j] != null)
                        {
                            _listContent[i] = _listContent[j];
                            _listContent[j] = default(T);
                            _count--;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            else
            {
                for (int i = _listContent.Length - 1; i >= 0; i--)
                {
                    if (_listContent[i] != null)
                    {
                        _listContent[index] = _listContent[i];
                        _listContent[i] = default(T);
                        _count--;
                        break;
                    }
                }
            }
        }

        public bool Contains(T inputItem)
        {
            bool containsItem = false;
            foreach (T item in _listContent)
            {
                if (item.Equals(inputItem))
                {
                    containsItem = true;
                    break;
                }
            }
            return containsItem;
        }

        public bool Exists(Predicate<T> match)
        {
            bool isExists = false;
            foreach (T item in _listContent)
            {
                if (match(item))
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }

        public T[] ToArray()
        {
            T[] array = new T[_listContent.Length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _listContent[i];
            }

            return array;
        }

        public void Reverse()
        {
            T[] array = new T[_listContent.Length];
            for (int i = _listContent.Length - 1; i >= 0; i--)
            {
                array[array.Length - 1 - i] = _listContent[i];
            }
            _listContent = array;
        }

        public T Find(Predicate<T> match)
        {
            T result = default(T);
            foreach (T item in _listContent)
            {
                if (match(item))
                {
                    result = item;
                }
            }
            return result;
        }

        public MyCustomList<T> FindAll(Predicate<T> match)
        {
            MyCustomList<T> result = new MyCustomList<T>();
            foreach (T item in _listContent)
            {
                if (match(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public T FindLast(Predicate<T> match)
        {
            T result = default(T);
            for (int i = _listContent.Length - 1; i >= 0; i--)
            {
                if (match(_listContent[i]))
                {
                    result = _listContent[i];
                    break;
                }
            }
            return result;
        }

        public int IndexOf(T element)
        {
            int index = -1;
            for (int i = 0; i < _listContent.Length; i++)
            {
                if(_listContent[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public int LastIndexOf(T element)
        {
            int index = -1;
            for (int i = _listContent.Length - 1; i <= 0; i--)
            {
                if (_listContent[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _listContent.Length; i++)
            {
                yield return _listContent[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _listContent.Length; i++)
            {
                yield return _listContent[i];
            }
        }
    }
}
