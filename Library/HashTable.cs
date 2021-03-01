using System;

namespace Library
{
    public class HashTable<Key, Value>
    {
        protected double LoadFactor = 0.8;
        protected double IncreaseSizeFactor = 0.8;
        protected double actualLoadFactor;
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
            double loadFactor = 0;
            foreach(Value value in array)
            {
                if(value != null)
                {
                    loadFactor++;
                }
            }
            actualLoadFactor = loadFactor;
        }

        private int Hash(Key key)
        {
            int hashCode = key.GetHashCode();
            int mask = 31 >> hashCode;
            hashCode ^= mask;
            hashCode -= mask;
            hashCode %= array.Length;
            return hashCode;
        }
    }
}
