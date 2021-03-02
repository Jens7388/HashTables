using System;
using System.Collections.Generic;

namespace Library
{
    public class HashTable<Key, Value>
    {
        protected readonly double loadFactor = 0.8;
        protected readonly double increaseSizeFactor = 1.5;
        protected double actualLoadFactor;
        protected int elements;
        protected LinkedList<KeyValuePair<Key, Value>>[] array;
        protected bool loadFactorThresholdReached; 

        public HashTable(int initialSize)
        {
            array = new LinkedList<KeyValuePair<Key, Value>>[initialSize];
            for(int i = 0; i < array.Length - 1; i++)
            {
                 array[i] = new();
            }          
        }


        public void Add(Key key, Value value)
        {
            if(loadFactorThresholdReached)
            {
                LinkedList<KeyValuePair<Key, Value>>[] newArray = new LinkedList<KeyValuePair<Key, Value>>[(int)(array.Length * increaseSizeFactor)];
                for(int i = 0; i < array.Length -1; i++)
                {
                    if(array[i].Count > 0)
                    {
                        foreach(KeyValuePair<Key, Value> item in array[i])
                        {
                            int newIndex = Hash(item.Key, newArray.Length);
                            newArray[newIndex].AddLast(item);
                        }
                    }
                }
                array = newArray;
                CalculateLoadFactor();
                int index = Hash(key, array.Length);
                array[index].AddLast(new KeyValuePair<Key, Value>( key, value));
            }
            else
            {
                int index = Hash(key, array.Length);
                array[index].AddLast(new KeyValuePair<Key, Value>(key, value));
                CalculateLoadFactor();
            }
        }

        public KeyValuePair<Key, Value> LookUp(Key key)
        {
            foreach(LinkedList<KeyValuePair<Key, Value>> list in array)
            {
                if(list.Count > 0)
                {
                    foreach(KeyValuePair<Key, Value> item in list)
                    {
                        if(item.Key.Equals(key))
                        {
                            return item;
                        }
                    }
                }
            }
            return default;
        }

        private void CalculateLoadFactor()
        {
            actualLoadFactor = 0;
            foreach(LinkedList<KeyValuePair<Key, Value>> item in array)
            {
                if(item.Count >= 0)
                {
                    actualLoadFactor++;
                }
            }
            actualLoadFactor /= array.Length;
            if(actualLoadFactor >= loadFactor)
            {
                loadFactorThresholdReached = true;
            }
            else
            {
                loadFactorThresholdReached = false;
            }
        }

        private int Hash(Key key, int arrayLength)
        {
            int hashCode = key.GetHashCode();
            int mask = hashCode >> 31;
            hashCode ^= mask;
            hashCode -= mask;
            hashCode %= arrayLength;
            return hashCode;
        }
    }
}
