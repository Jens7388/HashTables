using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ListHashTable<Key, Value> : HashTable<Key, Value>
    {
        public ListHashTable(int initialSize) : base(initialSize)
        {
            array = new BucketEntry<Key, Value>[initialSize];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = new();
            }
        }

        public override void Add(Key key, Value value)
        {
            if(loadFactorThresholdReached)
            {
                BucketEntry<Key, Value>[] newArray = new BucketEntry<Key, Value>[(int)(array.Length * increaseSizeFactor)];
                for(int i = 0; i < array.Length - 1; i++)
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
                array[index].Add(new KeyValuePair<Key, Value>(key, value));
            }
            else
            {
                int index = Hash(key, array.Length);
                array[index].Add(new KeyValuePair<Key, Value>(key, value));
                CalculateLoadFactor();
            }
        }

        public override KeyValuePair<Key, Value> LookUp(Key key)
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

        public override void CalculateLoadFactor()
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
    }
}
