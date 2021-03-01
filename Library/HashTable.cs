using System;

namespace Library
{
    public class HashTable<Key, Value>
    {
        protected double LoadFactor = 0.8;
        protected double IncreaseSizeFactor = 0.8;
        protected double ActualLoadFactor;
        protected int Elements;
        protected Value[] array;
        protected bool LoadFactorThresholdReached;

        public HashTable(int initialSize)
        {
            array= new Value[initialSize];
        }

        public void Add(Key key, Value value)
        {

        }

        public Value LookUp(Key key)
        {
            return default;
        }

        private void CalculateLoadFactor()
        {

        }

        private int Hash(Key key)
        {
            return default;
        }
    }
}
