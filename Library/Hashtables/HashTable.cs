using System;
using System.Collections.Generic;

namespace Library
{
    public abstract class HashTable<Key, Value>
    {
        protected readonly double loadFactor = 0.8;
        protected readonly double increaseSizeFactor = 1.5;
        protected double actualLoadFactor;
        protected int elements;
        protected BucketEntry<Key, Value>[] array;
        protected bool loadFactorThresholdReached; 

        public HashTable(int initialSize)
        {
            
        }


        public virtual void Add(Key key, Value value)
        {
            
        }

        public abstract KeyValuePair<Key, Value> LookUp(Key key);
        

        public virtual void CalculateLoadFactor()
        {
            
        }

        public virtual int Hash(Key key, int arrayLength)
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
