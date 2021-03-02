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
        protected KeyValuePair<Key, Value>[] array;
        protected bool loadFactorThresholdReached; 

        public HashTable(int initialSize)
        {
            array = new KeyValuePair<Key, Value>[initialSize];
        }


        public void Add(Key key, Value value)
        {
            if(loadFactorThresholdReached)
            {
                KeyValuePair<Key, Value>[] newArray = new KeyValuePair<Key, Value>[(int)(array.Length * increaseSizeFactor)];
                for(int i = 0; i < array.Length -1; i++)
                {
                    if(array[i].Key != null && array[i].Value != null)
                    {
                        int newIndex = Hash(key, newArray.Length);
                        newArray[newIndex] = array[i];
                    }
                }
                array = newArray;
                CalculateLoadFactor();
                int index = Hash(key, array.Length);
                array[index] = new KeyValuePair<Key, Value>( key, value);
            }
            else
            {
                int index = Hash(key, array.Length);
                array[index] = new KeyValuePair<Key, Value>(key, value);
            }
        }

        public Value LookUp(Key key)
        {
            return default;
        }

        private void CalculateLoadFactor()
        {
            actualLoadFactor = 0;
            foreach(KeyValuePair<Key, Value> item in array)
            {
                if(item.Key != null && item.Value != null)
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
