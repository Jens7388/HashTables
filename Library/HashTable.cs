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
        protected BucketEntry<Key, Value>[] array;
        protected bool loadFactorThresholdReached; 

        public HashTable(int initialSize)
        {
            array = new BucketEntry<Key, Value>[initialSize];
            for(int i = 0; i < array.Length - 1; i++)
            {
                 array[i] = new();
            }          
        }


        public void Add(Key key, Value value)
        {
            if(loadFactorThresholdReached)
            {
                BucketEntry<Key, Value>[] newArray = new BucketEntry<Key, Value>[(int)(array.Length * increaseSizeFactor)];
                for(int i = 0; i < array.Length -1; i++)
                {
                    if(array[i].Head.Key != null && array[i].Head.Value != null)
                    {
                        int newIndex = Hash(array[i].Head.Key, newArray.Length);
                        newArray[newIndex] = array[i];
                    }
                    if(array[i].Items.Count > 0)
                    {
                        foreach(KeyValuePair<Key, Value> item in array[i].Items)
                        {
                            int newIndex = Hash(item.Key, newArray.Length);
                            newArray[newIndex].Add(item);
                        }
                    }
                }
                array = newArray;
                CalculateLoadFactor();
                int index = Hash(key, array.Length);
                array[index].Add(new KeyValuePair<Key, Value>( key, value));
            }
            else
            {
                int index = Hash(key, array.Length);
                array[index].Add(new KeyValuePair<Key, Value>(key, value));
                CalculateLoadFactor();
            }
        }

        public KeyValuePair<Key, Value> LookUp(Key key)
        {
            foreach(BucketEntry<Key, Value> item in array)
            {
                if(item.Head.Key.Equals(key))
                {
                    return item.Head;
                }
                else
                {
                    foreach(KeyValuePair<Key, Value> listItem in item.Items)
                    {
                        if(listItem.Key.Equals(key))
                        {
                            return listItem;
                        }
                    }
                }
            }
            return default;
        }

        private void CalculateLoadFactor()
        {
            actualLoadFactor = 0;
            foreach(BucketEntry<Key, Value> item in array)
            {
                if(item.Head.Key != null && item.Head.Value != null)
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
